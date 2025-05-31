import { Eye, EyeOff, Lock, LogIn, Mail, User, UserPlus } from 'lucide-react';
import { useState } from 'react';
import { LoginAccount, RegisterAccount } from '../api/auth';
import { useNavigate } from 'react-router';

export default function AuthForm() {
  const navigate = useNavigate();
  const [isLogin, setIsLogin] = useState(true);
  const [formData, setFormData] = useState({ hoTen: '', email: '', matKhau: '' });
  const [showPassword, setShowPassword] = useState(false);
  const [errors, setErrors] = useState({});
  const [isLoading, setIsLoading] = useState(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));    // Clear error when user starts typing
    if (errors[name]) setErrors(prev => ({ ...prev, [name]: '' }));
  };

  const validateForm = () => {
    const newErrors = {};

    // Validate full name only for signup
    if (!isLogin && !formData.hoTen.trim()) newErrors.fullName = 'Vui lòng nhập họ và tên';

    if (!formData.email.trim()) newErrors.email = 'Vui lòng nhập email';
    // else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) newErrors.email = 'Email không hợp lệ';

    if (!formData.matKhau) newErrors.password = 'Vui lòng nhập mật khẩu';
    // else if (!isLogin && formData.password.length < 6) newErrors.password = 'Mật khẩu phải có ít nhất 6 ký tự';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) return;

    const action = isLogin ? 'Đăng nhập' : 'Đăng ký';
    setIsLoading(true);
    try {

      const result = isLogin ? await LoginAccount(formData) : await RegisterAccount(formData);
      setIsLoading(false);

      if (result?.status >= 400) {
        alert(result?.data?.message || `${action} thất bại!`);
        return;
      }
      // Save token to localStorage if login is successful
      isLogin && localStorage.setItem('token', result);
      alert(`${action} thành công!`);
      isLogin ? navigate('/') : navigate('/auth');
    } catch {
      alert("Có lỗi xảy ra, vui lòng thử lại!");
    }

    setIsLoading(false);

    setFormData({ hoTen: '', email: '', matKhau: '' });
    // navigate('/auth')
  };

  const toggleMode = () => {
    setIsLogin(!isLogin);
    setErrors({});
    setFormData({ hoTen: '', email: '', matKhau: '' });
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 flex items-center justify-center p-4">
      <div className="max-w-md w-full bg-white rounded-2xl shadow-xl p-8">
        <div className="text-center mb-8">
          <div className={`mx-auto w-16 h-16 rounded-full flex items-center justify-center mb-4 ${isLogin ? 'bg-green-100' : 'bg-blue-100'}`}>
            {isLogin ? <LogIn className={`w-8 h-8 ${isLogin ? 'text-green-600' : 'text-blue-600'}`} /> : <UserPlus className={`w-8 h-8 ${isLogin ? 'text-green-600' : 'text-blue-600'}`} />}
          </div>
          <h1 className="text-2xl font-bold text-gray-900">
            {isLogin ? 'Đăng nhập' : 'Đăng ký tài khoản'}
          </h1>
          <p className="text-gray-600 mt-2">
            {isLogin ? 'Chào mừng bạn trở lại' : 'Tạo tài khoản mới để bắt đầu'}
          </p>
        </div>

        <div className="space-y-6">
          {!isLogin && (
            <div className="transform transition-all duration-300 ease-in-out">
              <label htmlFor="fullName" className="block text-sm font-medium text-gray-700 mb-2">Họ và tên</label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <User className="h-5 w-5 text-gray-400" />
                </div>
                <input type="text" id="fullName" name="hoTen" value={formData.hoTen} onChange={handleInputChange} placeholder="Nhập họ và tên của bạn"
                  className={`w-full pl-10 pr-4 py-3 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors ${errors.fullName ? 'border-red-300' : 'border-gray-300'}`} />
              </div>
              {errors.fullName && <p className="mt-1 text-sm text-red-600">{errors.fullName}</p>}
            </div>
          )}

          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-2">Email</label>
            <div className="relative">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <Mail className="h-5 w-5 text-gray-400" />
              </div>
              <input type="email" id="email" name="email" value={formData.email} onChange={handleInputChange} placeholder="Nhập địa chỉ email"
                className={`w-full pl-10 pr-4 py-3 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors ${errors.email ? 'border-red-300' : 'border-gray-300'}`} />
            </div>
            {errors.email && <p className="mt-1 text-sm text-red-600">{errors.email}</p>}
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700 mb-2">Mật khẩu</label>
            <div className="relative">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <Lock className="h-5 w-5 text-gray-400" />
              </div>
              <input type={showPassword ? 'text' : 'password'} id="password" name="matKhau" value={formData.matKhau} onChange={handleInputChange} placeholder="Nhập mật khẩu"
                className={`w-full pl-10 pr-12 py-3 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors ${errors.password ? 'border-red-300' : 'border-gray-300'}`} />
              <button type="button" onClick={() => setShowPassword(!showPassword)} className="absolute inset-y-0 right-0 pr-3 flex items-center">
                {showPassword ? <EyeOff className="h-5 w-5 text-gray-400 hover:text-gray-600" /> : <Eye className="h-5 w-5 text-gray-400 hover:text-gray-600" />}
              </button>
            </div>
            {errors.password && <p className="mt-1 text-sm text-red-600">{errors.password}</p>}
          </div>

          <button type="button" onClick={handleSubmit} disabled={isLoading}
            className={`w-full py-3 px-4 rounded-lg font-medium text-white transition-colors ${isLoading
              ? 'bg-gray-400 cursor-not-allowed' : isLogin
                ? 'bg-green-600 hover:bg-green-700 focus:ring-2 focus:ring-green-500 focus:ring-offset-2'
                : 'bg-blue-600 hover:bg-blue-700 focus:ring-2 focus:ring-blue-500 focus:ring-offset-2'}`}>
            {isLoading ? (
              <div className="flex items-center justify-center">
                <div className="animate-spin rounded-full h-5 w-5 border-b-2 border-white mr-2"></div>
                {isLogin ? 'Đang đăng nhập...' : 'Đang đăng ký...'}
              </div>
            ) : (isLogin ? 'Đăng nhập' : 'Đăng ký')}
          </button>
        </div>

        <div className="mt-6">
          <div className="relative">
            <div className="absolute inset-0 flex items-center">
              <div className="w-full border-t border-gray-300" />
            </div>
            <div className="relative flex justify-center text-sm">
              <span className="px-2 bg-white text-gray-500">hoặc</span>
            </div>
          </div>

          <div className="mt-6 text-center">
            <p className="text-sm text-gray-600">
              {isLogin ? 'Chưa có tài khoản?' : 'Đã có tài khoản?'}{' '}
              <button onClick={toggleMode} className={`font-medium hover:underline transition-colors ${isLogin ? 'text-blue-600 hover:text-blue-500' : 'text-green-600 hover:text-green-500'}`}>
                {isLogin ? 'Đăng ký ngay' : 'Đăng nhập ngay'}
              </button>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}