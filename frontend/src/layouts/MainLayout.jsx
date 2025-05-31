import { Outlet, useNavigate } from "react-router";
import { BookingContext, initialValue, reducer } from "../context/BookingContext";
import { useReducer } from "react";
import { Button } from "antd";

function MainLayout() {
  const [state, dispatch] = useReducer(reducer, initialValue)
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  return (
    <BookingContext.Provider value={[state, dispatch]}>
      <div className="min-h-screen bg-gradient-to-br from-red-50 via-pink-50 to-orange-50 py-8 relative">
        {token ?
          <div className="absolute right-10 top-10">
            <Button type="primary" className="bg-red-600 hover:bg-red-700 border-0"
              onClick={() => {
                dispatch({ type: "resetBooking" })
                localStorage.removeItem("token");
              }}>
              Đăng xuất
            </Button>
          </div> :
          <div className="absolute right-10 top-10">
            <Button type="primary" className="bg-red-600 hover:bg-red-700 border-0"
              onClick={() => {
                dispatch({ type: "resetBooking" })
                navigate("/auth");
              }}>
              Đăng nhập
            </Button>
          </div>}
        <div className="container mx-auto px-4 max-w-6xl">
          <Outlet />
        </div>
      </div>
    </BookingContext.Provider>
  )
}

export default MainLayout