using IdentityServer4.Events;
using IdentityServer4.Services;
using MassTransit;
using System.Threading.Tasks;

namespace IdentityServer.Host
{
    public class EventSink: IEventSink
    {
        private readonly IBusControl _bus;
        private ISendEndpoint _sendEndPoint;
        public EventSink(IBusControl bus)
        {
            _bus = bus;
        }
        public async Task PersistAsync(Event evt)
        {
            switch(evt)
            {                
                case ApiAuthenticationFailureEvent apiFailure:
                    {
                        
                    }
                    break;
                case ApiAuthenticationSuccessEvent apiSuccess:
                    {
                        
                    }
                    break;
                case ClientAuthenticationFailureEvent clientFailure:
                    {

                    }
                    break;
                case ClientAuthenticationSuccessEvent clientSuccess:
                    {

                    }
                    break;
                case ConsentDeniedEvent denied:
                    {

                    }
                    break;
                case ConsentGrantedEvent granted:
                    {

                    }
                    break;
                case DeviceAuthorizationFailureEvent deviceFailure:
                    {

                    }
                    break;
                case DeviceAuthorizationSuccessEvent deviceSuccess:
                    {

                    }
                    break;
                case GrantsRevokedEvent revoked:
                    {

                    }
                    break;
                case InvalidClientConfigurationEvent invalid:
                    {

                    }
                    break;
                case TokenIntrospectionFailureEvent tokenFailure:
                    {

                    }
                    break;
                case TokenIntrospectionSuccessEvent tokenSuccess:
                    {

                    }
                    break;
                case TokenIssuedFailureEvent issueFailure:
                    {

                    }
                    break;
                case TokenIssuedSuccessEvent issueSuccess:
                    {

                    }
                    break;
                case TokenRevokedSuccessEvent revokeSuccess:
                    {

                    }
                    break;
                case UnhandledExceptionEvent exception:
                    {

                    }
                    break;
                case UserLoginFailureEvent loginFailure:
                    {

                    }
                    break;
                case UserLoginSuccessEvent loginSuccess:
                    {

                    }
                    break;
                case UserLogoutSuccessEvent logoutSuccess:
                    {

                    }
                    break;
            }
            await Task.CompletedTask;
        }
    }
}
