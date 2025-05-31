import { Button, Card, Divider, Typography } from 'antd';
import { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router';

import { BookingContext, getTotalPrice } from '../context/BookingContext';
import { OrderTicket } from '../api/movie';

const { Title, Text } = Typography;

function CustomerInfo() {
  const navigate = useNavigate()
  const [{
    selectedMovie, selectedShowtime, selectedSeats, seats
  },] = useContext(BookingContext)
  const [loading, setLoading] = useState(false);

  useEffect(() => {

    if (!selectedMovie || !selectedShowtime || selectedSeats.length === 0) navigate("/");
  }, [navigate, selectedMovie, selectedSeats.length, selectedShowtime])


  const handleBooking = async () => {
    const seatList = seats
      .filter(seat => selectedSeats.includes(seat.id))
      .map(seat => ({ hang: seat.soHang, cot: seat.soCot, maLoaiGhe: seat.loaiGhe?.maLoaiGhe }));
    const data = {
      maCaChieu: selectedShowtime?.maCaChieu,
      ghe: seatList
    }
    setLoading(true);

    await OrderTicket(data)
      .then(res => {
        console.log("Kết quả đặt vé:", res);
        if (res.status >= 300) {

          alert('Có lỗi xảy ra, vui lòng thử lại!');
          return
        }
        alert('Đặt vé thành công!');
        navigate("/success");
      }).catch(err => {
        console.error("Lỗi khi đặt vé:", err);
        alert('Có lỗi xảy ra, vui lòng thử lại!');
      })
    setLoading(false);
  }


  return (
    <div className="space-y-6">
      <div className="text-center mb-8">
        <Title level={3}>Thông tin khách hàng</Title>
        <Text className="text-gray-500">Vui lòng nhập thông tin để hoàn tất đặt vé</Text>
      </div>

      <Card className="mb-6">
        <Title level={4} className="mb-4">Thông tin đặt vé</Title>
        <div className="space-y-3">
          <div className="flex justify-between">
            <Text>Phim:</Text>
            <Text strong>{selectedMovie?.tenPhim}</Text>
          </div>
          <div className="flex justify-between">
            <Text>Suất chiếu:</Text>
            <Text strong>{new Date(selectedShowtime?.thoiGianChieu).toLocaleString()} - {selectedShowtime?.tenLoaiHinh}</Text>
          </div>
          <div className="flex justify-between">
            <Text>Ghế:</Text>
            <Text strong>{selectedSeats.join(', ')}</Text>
          </div>
          <Divider />
          <div className="flex justify-between text-lg">
            <Text strong>Tổng tiền:</Text>
            <Text strong className="text-red-600">{getTotalPrice(selectedSeats, seats)?.toLocaleString()}đ</Text>
          </div>
        </div>
      </Card>

      <Button type="primary" size="large" loading={loading} onClick={handleBooking}
        className="w-full mt-3 h-12 text-lg font-semibold bg-red-600 hover:bg-red-700 border-0">
        {loading ? 'Đang xử lý...' : `Đặt vé - ${getTotalPrice(selectedSeats, seats).toLocaleString()}đ`}
      </Button>
    </div>
  );
}


export default CustomerInfo