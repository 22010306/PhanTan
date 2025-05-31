import { BrowserRouter, Route, Routes } from "react-router"

import MainLayout from "./layouts/MainLayout"
import AuthForm from "./pages/AuthForm"
import CustomerInfo from "./pages/CustomerInfo"
import MovieSelection from "./pages/MovieSelection"
import ShowtimeSelection from "./pages/ShowtimeSelection"
import SuccessPage from "./pages/SuccessPage"

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/auth" element={<AuthForm />} />
        <Route path="/" element={<MainLayout />}>
          <Route path="/" element={<MovieSelection />} />
          <Route path="/showtime" element={<ShowtimeSelection />} />
          <Route path="/customer" element={<CustomerInfo />} />
          <Route path="/success" element={<SuccessPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
