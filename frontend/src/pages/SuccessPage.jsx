import { Button, Card, Space, Typography } from 'antd';
import { CheckCircle } from 'lucide-react';
import { useContext } from 'react';
import { useNavigate } from 'react-router';

import { useEffect } from 'react';
import { BookingContext, getTotalPrice } from '../context/BookingContext';

const { Title, Text } = Typography;

function SuccessPage() {
  const navigate = useNavigate(); // Placeholder for navigation, replace with actual navigation if needed
  const [{ selectedMovie, selectedShowtime, selectedSeats, seats }, dispatch] = useContext(BookingContext); // Placeholder for context, replace with actual context if needed

  useEffect(() => {
    if (!selectedMovie || !selectedShowtime || selectedSeats.length === 0) {
      navigate('/'); // Navigate to home or movie selection page
    }
  }, [navigate, selectedMovie, selectedShowtime, selectedSeats.length]);

  return (
    <Card className="shadow-lg text-center border-0">
      <div className="py-12">
        <div className="flex justify-center mb-6">
          <div className="bg-green-100 p-6 rounded-full">
            <CheckCircle className="w-16 h-16 text-green-500" />
          </div>
        </div>
        <Title level={2} className="text-green-600 mb-4">
          Đặt vé thành công!
        </Title>
        <Text className="text-lg text-gray-600 mb-8 block max-w-md mx-auto">
          Vé điện tử đã được gửi đến email của bạn.
          Vui lòng mang theo mã vé để vào rạp.
        </Text>

        <div className="bg-gray-50 p-6 rounded-lg mb-8 max-w-md mx-auto">
          <Title level={4} className="mb-4">Thông tin vé</Title>
          <div className="space-y-2 text-left">
            <div className="flex justify-between">
              <Text>Mã vé:</Text>
              <Text strong>#{Math.random().toString(36).substr(2, 9).toUpperCase()}</Text>
            </div>
            <div className="flex justify-between">
              <Text>Phim:</Text>
              <Text strong>{selectedMovie?.tenPhim}</Text>
            </div>
            <div className="flex justify-between">
              <Text>Suất chiếu:</Text>
              <Text strong>{new Date(selectedShowtime?.thoiGianChieu).toLocaleString()}</Text>
            </div>
            <div className="flex justify-between">
              <Text>Ghế:</Text>
              <Text strong>{selectedSeats.join(', ')}</Text>
            </div>
            <div className="flex justify-between">
              <Text>Tổng tiền:</Text>
              <Text strong className="text-red-600">{getTotalPrice(selectedSeats, seats).toLocaleString()}đ</Text>
            </div>
          </div>
        </div>

        <Space size="large">
          <Button type="primary" size="large" className="bg-red-600 hover:bg-red-700 border-0"
            onClick={() => {
              navigate('/'); // Navigate to home or movie selection page
              dispatch({ type: 'resetBooking' }); // Reset booking context
            }}>Đặt vé mới</Button>
          <Button size="large" className="border-red-300 text-red-600 hover:border-red-500">
            Tải vé PDF
          </Button>
        </Space>
      </div>
    </Card>
  );

}

export default SuccessPage