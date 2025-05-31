import { Button, Modal, Typography } from "antd";
import { Armchair } from "lucide-react";
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router";

import { GetLoaiGhe, GetOrderedSeat } from "../api/movie";
import { BookingContext, generateSeats, getTotalPrice } from "../context/BookingContext";

const { Title, Text } = Typography;

function SeatSelection() {
  const navigate = useNavigate()
  const [{ showSeatModal, selectedSeats, selectedShowtime, seats, gheDaDat }, dispatch] = useContext(BookingContext)
  const [loaiGhe, setLoaiGhe] = useState([])

  // if (!selectedShowtime) navigate("/");
  useEffect(function () {
    GetLoaiGhe().then(res => {
      setLoaiGhe(res)
    }).catch(err => {
      console.error("Lỗi khi lấy loại ghế:", err);
    })
  }, [selectedShowtime?.maCaChieu])


  useEffect(() => {
    dispatch({ type: "updateSeats", payload: generateSeats(selectedShowtime, loaiGhe, gheDaDat) });
  }, [dispatch, gheDaDat, loaiGhe, selectedShowtime]);

  const handleSeatSelect = (seatId) => {
    const seat = seats.find(s => s.id === seatId);
    if (seat.isOccupied) return;

    dispatch({ type: "updateSelectedSeat", payload: seatId })
  };

  return (
    <Modal title="Chọn ghế ngồi" open={showSeatModal} width={800}
      onCancel={() => {
        dispatch({ type: "updateSeatModel", payload: false })
        dispatch({ type: "clearSeatSelection" });
      }}
      footer={[
        <Button key="back" onClick={() => {
          dispatch({ type: "updateSeatModel", payload: false })
          dispatch({ type: "clearSeatSelection" });
        }}>
          Hủy
        </Button>,
        <Button key="submit" type="primary" disabled={selectedSeats.length === 0} className="bg-red-600 hover:bg-red-700 border-0"
          onClick={() => {
            dispatch({ type: "updateSeatModel", payload: false });
            navigate("/customer")
          }}>
          Xác nhận ({selectedSeats.length} ghế)
        </Button>
      ]}
    >
      <div className="space-y-6">
        <div className="text-center">
          <div className="bg-gray-800 text-white py-2 px-35 rounded-lg inline-block mb-4">
            MÀN HÌNH
          </div>
        </div>

        <div className="flex justify-center">
          <div className={[`grid gap-3 `].join(' ')} style={{ gridTemplateColumns: `repeat(${selectedShowtime?.soCot}, 1fr)` }}>
            {seats.map(seat => (
              <button
                key={seat.id}
                disabled={seat.isOccupied}
                onClick={() => handleSeatSelect(seat.id)}
                className={[
                  `w-8 h-8 flex justify-center items-center rounded-md border-2 text-xs font-medium transition-all`,
                  seat.isOccupied
                    ? 'bg-gray-300 border-gray-300 cursor-not-allowed'
                    : selectedSeats.includes(seat.id)
                      ? 'bg-red-500 border-red-500 text-white'
                      : seat.isVIP
                        ? 'bg-yellow-100 border-yellow-400 hover:bg-yellow-200'
                        : 'bg-green-100 border-green-400 hover:bg-green-200'
                ].join(' ')}
                title={`Ghế ${seat.id} - ${seat.giaTien?.toLocaleString()}đ`}>
                <Armchair className="w-4 h-4" />
              </button>
            ))}
          </div>
        </div>

        <div className="flex justify-center gap-6 text-sm">
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-green-100 border-2 border-green-400 rounded"></div>
            <Text>Thường ({(loaiGhe[0]?.giaTien || 0).toLocaleString()}đ)</Text>
          </div>
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-yellow-100 border-2 border-yellow-400 rounded"></div>
            <Text>VIP ({(loaiGhe[1]?.giaTien || 0).toLocaleString()}đ)</Text>
          </div>
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-gray-300 border-2 border-gray-300 rounded"></div>
            <Text>Đã đặt</Text>
          </div>
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-red-500 border-2 border-red-500 rounded"></div>
            <Text>Đang chọn</Text>
          </div>
        </div>

        {selectedSeats.length > 0 && (
          <div className="bg-red-50 p-4 rounded-lg">
            <div className="flex justify-between items-center">
              <Text>Ghế đã chọn: <strong>{selectedSeats.join(', ')}</strong></Text>
              <Text strong className="text-red-600 text-lg">
                {getTotalPrice(selectedSeats, seats)?.toLocaleString()}đ
              </Text>
            </div>
          </div>
        )}
      </div>
    </Modal>
  );
}


export default SeatSelection