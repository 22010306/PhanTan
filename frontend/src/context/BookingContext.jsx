import { createContext } from "react";

export const BookingContext = createContext()

export const initialValue = {
  selectedMovie: null,
  movies: [],
  showSeatModal: false,
  selectedShowtime: null,
  selectedSeats: [],
  seats: [],
  gheDaDat: [],
  formData: {
    customerName: '',
    phone: '',
    email: '',
    paymentMethod: ''
  }
}

const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
export const generateSeats = (selectedShowtime, loaiGhe) => {
  const seats = [];
  const rows = alphabet.slice(0, selectedShowtime?.soHang).split('');
  console.log("selectedShowtime", selectedShowtime)
  for (let row of rows) {
    let rowIndex = alphabet.indexOf(row);
    for (let num = 0; num < selectedShowtime?.soCot; num++) {

      const isVIP = 3 <= rowIndex && rowIndex < 6;
      seats.push({
        id: `${row} ${num}`,
        soCot: alphabet.indexOf(row),
        soHang: num,
        isVIP,
        isOccupied: selectedShowtime.ghe.some(ghe => ghe.hang === num && ghe.cot === alphabet.indexOf(row)),
        loaiGhe: (isVIP ? loaiGhe?.[1] : loaiGhe?.[0]),
        giaTien: (isVIP ? loaiGhe?.[1] : loaiGhe?.[0])?.giaTien
      });
    }
  }
  return seats;
};

export const getTotalPrice = (selectedSeats, seats) => {
  return selectedSeats?.reduce((total, seatId) => {
    const seat = seats.find(s => s.id === seatId);
    return total + seat?.giaTien || 0;
  }, 0);
};

export const reducer = (state, action) => {
  const _state = { ...state }

  switch (action?.type) {
    case "updateFormData":
      _state.formData = { ..._state.formData, gheDaDat: [...action.payload] }
      break;
    case "updateSeats":
      _state.seats = action.payload
      break;
    case "clearSeatSelection":
      _state.selectedSeats = []
      break;
    case "updateMovieList":
      _state.movies = action.payload ?? []
      break;
    case "updateSelectedMovie":
      _state.selectedMovie = action.payload
      break;
    case "updateSelectedShowtime":
      _state.selectedShowtime = action.payload
      break;
    case "updateSelectedSeat":
      _state.selectedSeats = _state.selectedSeats.includes(action.payload) ?
        _state.selectedSeats.filter(seat => seat !== action.payload) :
        [..._state.selectedSeats, action.payload]
      break;
    case "updateSeatModel":
      _state.showSeatModal = action.payload
      break
    case "updatePaymentMethod":
      _state.paymentMethod = action.payload
      break
    case "resetBooking":
      _state.selectedMovie = null
      _state.selectedShowtime = null
      _state.selectedSeats = []
      _state.formData = { ...initialValue.formData }
      _state.showSeatModal = false
      break;
  }
  return _state
}

