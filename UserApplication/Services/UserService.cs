using UserApplication.ServiceInterfaces;
using UserDomain;
using UserDomain.Repository;

namespace UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <exception cref="InvalidOperationException"></exception>
        public async Task<User> Add(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.Save();
                return user;
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return (await _userRepository.GetAllAsync()).ToList();
        }

        public async Task<User> GetById(int id)
        {
            var result = await _userRepository.GetAsync(id);

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

    }
}
