import { Badge, Button, Card, Typography } from 'antd';
import { Clock } from 'lucide-react';
import { useContext } from 'react';
import { useNavigate } from 'react-router';

import { BookingContext } from '../context/BookingContext';
import SeatSelection from './SeatSelection';
import { GetOrderedSeat } from '../api/movie';

const { Title, Text } = Typography;

function ShowtimeSelection() {
  const navigate = useNavigate()
  const [{ selectedMovie, selectedShowtime }, dispatch] = useContext(BookingContext)

  if (!selectedMovie) navigate("/");

  return (
    <>
      <div className="space-y-6">
        <div className="flex items-center justify-between mb-6">
          <div>
            <Title level={3}>Chọn suất chiếu</Title>
            {/* <Text className="text-gray-500">{selectedMovie?.tenPhim} - {selectedMovie?.doDai}</Text> */}
          </div>
          <Button onClick={() => navigate("/")}>Đổi phim</Button>
        </div>

        <Card className="mb-6">
          <div className="flex items-center gap-4 mb-4">
            <div className="text-4xl">{selectedMovie?.poster}</div>
            <div>
              <Title level={4} className="mb-1">{selectedMovie?.tenPhim}</Title>
              <Text className="text-gray-500">{selectedMovie?.doDai} phút</Text>
            </div>
          </div>
        </Card>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mt-10">
          {selectedMovie?.caChieu?.map(showtime => (
            <Card key={showtime.maCaChieu} hoverable
              className={`cursor-pointer transition-all ${selectedShowtime?.maCaChieu === showtime?.maCaChieu ? 'ring-2 ring-red-500 bg-red-50' : 'hover:shadow-md'}`}
              onClick={async () => {
                dispatch({ type: "updateSelectedShowtime", payload: showtime })
                // const result = await GetOrderedSeat(showtime.maCaChieu);
                // dispatch({ type: "updateOrderedSeat", payload: result });
                dispatch({ type: "updateSeatModel", payload: true });
                dispatch({ type: "clearSeatSelection" });
              }}>
              <div className="text-center">
                <div className="flex justify-center items-center gap-2 mb-2">
                  <Clock className="w-5 h-5 text-red-600" />
                  <p className="text-black font-semibold text-xl" level={5}>{new Date(showtime?.thoiGianChieu).toLocaleTimeString()}</p>
                </div>
                <Badge count={showtime?.tenLoaiHinh}
                  style={{ backgroundColor: showtime?.tenLoaiHinh === 'IMAX' ? '#722ed1' : showtime?.loai === '3D' ? '#1890ff' : '#52c41a' }} className="mb-2" />
                <div className="space-y-1">
                  {/* <Text strong className="block text-lg text-red-600">{showtime?.giaVe?.toLocaleString()}đ</Text> */}
                  <Text className="text-sm text-gray-500">Còn {showtime?.soHang * showtime?.soCot} ghế</Text>
                </div>
              </div>
            </Card>
          ))}
        </div>

        {selectedShowtime &&
          <Button className="bg-red-600 hover:bg-red-700 border-0" type="primary" size="large"
            onClick={() => dispatch({ type: "updateSeatModel", payload: true })} >
            Chọn ghế ngồi
          </Button>}
      </div>

      <SeatSelection />
    </>
  );

}

export default ShowtimeSelection