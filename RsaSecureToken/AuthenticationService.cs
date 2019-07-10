using System;

namespace RsaSecureToken
{
    public interface IProfileDao
    {
        int GetRegisterTimeInMinutes(string account);
    }

    public interface IRsaTokenDao
    {
        Random GetRandom(int minutes);
    }

    public class AuthenticationService
    {
        private IProfileDao _profileDao;
        private IRsaTokenDao _rsaTokenDao;

        public AuthenticationService()
        {
            _profileDao = new ProfileDao();
            _rsaTokenDao = new RsaTokenDao();
        }

        public AuthenticationService(IProfileDao profileDao, IRsaTokenDao rsaTokenDao)
        {
            _profileDao = profileDao;
            _rsaTokenDao = rsaTokenDao;
        }

        public bool IsValid(string account, string password)
        {
            // 根據 account 取得設定時間
            var profileDao = _profileDao;
            var registerMinutes = profileDao.GetRegisterTimeInMinutes(account);

            // 根據 registerMinutes 取得 RSA token 目前的亂數
            var rsaToken = _rsaTokenDao;
            var seed = rsaToken.GetRandom(registerMinutes);

            // 驗證傳入的 password 是否等於 otp
            var isValid = password == seed.Next(0, 999999).ToString("000000"); ;

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ProfileDao : IProfileDao
    {
        public virtual int GetRegisterTimeInMinutes(string account)
        {
            // Not Complete yet
            throw new NotImplementedException();
        }
    }

    public class RsaTokenDao : IRsaTokenDao
    {
        public virtual Random GetRandom(int minutes)
        {
            return new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMinutes - minutes);
        }
    }
}