using ServerApiDependency.Enum;
using ServerApiDependency.Enums;
using ServerApiDependency.Interface;
using ServerApiDependency.Utility;
using ServerApiDependency.Utility.CustomException;
using System;
using System.Net;

namespace ServerApiDependency
{
    public class ServerApi : IServerApi
    {
        public ServerResponse CancelGame()
        {
            const string apiPage = "cancel.php";
            try
            {
                var response = Response(apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    TiDebug(apiPage, response);
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }

                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                TiDebugHelper.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.CancelGame, apiPage);
                throw e;
            }
        }

        public ServerResponse GameResult()
        {
            const string apiPage = "result.php";
            try
            {
                var response = PostToThirdParty(ApiType.GameResult, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    TiDebugHelper.Error($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                TiDebugHelper.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.GameResult, apiPage);
                throw e;
            }
        }

        public ServerResponse StartGame()
        {
            const string apiPage = "start.php";
            try
            {
                var response = PostToThirdParty(ApiType.StartGame, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    TiDebugHelper.Error($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                TiDebugHelper.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.StartGame, apiPage);
                throw e;
            }
        }

        internal virtual int Response(string apiPage)
        {
            return PostToThirdParty(ApiType.CancelGame, apiPage);
        }

        internal virtual void TiDebug(string apiPage, int response)
        {
            TiDebugHelper.Error($"{apiPage} response has error, ErrorCode = {response}");
        }

        /// <summary>
        /// treat this method is a dependency to connect to third-party server
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private int PostToThirdParty(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// treat this method as a dependency to connect to db
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SaveFailRequestToDb(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }
    }
}