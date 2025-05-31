import { Button, Card, Col, Row, Tag, Typography } from 'antd';
import { Film, Star } from 'lucide-react';
import { useContext, useEffect } from 'react';
import { useNavigate } from 'react-router';

import { BookingContext } from '../context/BookingContext';
import { GetMovieList } from '../api/movie';

const { Title, Text } = Typography;

function MovieSelection() {
  const navidate = useNavigate()
  const [{ selectedMovie, movies }, dispatch] = useContext(BookingContext)

  useEffect(function () {
    dispatch({ type: "updateSelectedMovie", payload: null });
    GetMovieList().then(res => {
      dispatch({ type: "updateMovieList", payload: res });
    }).catch(err => {
      console.error("Error fetching movie list:", err);
    })
  }, [dispatch])

  return (
    <div className="space-y-6">
      <div className="text-center mb-8">
        <div className="flex justify-center mb-4">
          <div className="bg-red-100 p-4 rounded-full">
            <Film className="w-8 h-8 text-red-600" />
          </div>
        </div>
        <Title level={2} className="text-red-600 mb-2">Chọn phim</Title>
        <Text className="text-gray-500">Khám phá những bộ phim hay nhất đang chiếu</Text>
      </div>

      <Button disabled={selectedMovie === null} className="bg-red-600 hover:bg-red-700 border-0" type="primary" size="large"
        onClick={() => navidate("/showtime")}>
        Chọn suất chiếu
      </Button>
      <Row gutter={[24, 24]}>
        {movies?.map?.(movie => (
          <Col xs={24} md={8} key={movie?.maPhim}>
            <Card hoverable className={`h-full transition-all duration-300 ${selectedMovie?.maPhim === movie?.maPhim ? 'ring-2 ring-red-500 shadow-xl' : 'hover:shadow-lg'}`}
              onClick={() => dispatch({ type: "updateSelectedMovie", payload: movie })}>
              <div className="text-center mb-4">
                {/* <div className="text-6xl mb-3">{movie.poster}</div> */}
                <Title level={4} className="mb-2">{movie?.tenPhim}</Title>
                <div className="flex justify-center items-center gap-2 mb-2">
                  <Star className="w-4 h-4 text-yellow-500 fill-current" />
                  <Text strong className="text-yellow-600">{movie?.danhGia.toFixed(1)}</Text>
                </div>
                <Text className="text-gray-500 block mb-1">{movie.theLoai.map(i => i.tenTheLoai).join(", ")}</Text>
                <Text className="text-gray-500 block">{movie?.doDai} phút</Text>
              </div>
              {/* <Text className="text-gray-600 text-sm block mb-4">{movie?.description}</Text> */}
              <div className="flex flex-wrap gap-1">
                {movie?.caChieu.slice(0, 3).map(showtime => <Tag key={showtime.maCaChieu} color="blue">{new Date(showtime.thoiGianChieu).toLocaleTimeString()}</Tag>)}
                {movie?.caChieu.length > 3 && <Tag>+{movie?.caChieu.length - 3}</Tag>}
              </div>
            </Card>
          </Col>
        ))}
      </Row>

    </div>
  );
}

export default MovieSelection