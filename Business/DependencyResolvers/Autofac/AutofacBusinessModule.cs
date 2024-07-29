using System.Diagnostics;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.HttpAccessor.Abstract;
using Core.HttpAccessor.Concrete;
using Core.Utilities.Hcp;
using Core.Utilities.Interceptors;
using Core.Utilities.Mail.Smtp.Abstract;
using Core.Utilities.Mail.Smtp.Concrete;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Concrete.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Sftp.Abstract;
using Sftp.Concrete;
using Tandem.Abstract;
using Tandem.Concrete;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<ApiLogManager>().As<IApiLogService>();
            builder.RegisterType<EFApiLogDal>().As<IApiLogDal>();

            builder.RegisterType<CorporateMailAddressManager>().As<ICorporateMailAddressService>();
            builder.RegisterType<EFCorporateMailAddressDal>().As<ICorporateMailAddressDal>();

            builder.RegisterType<CorporateManager>().As<ICorporateService>();
            builder.RegisterType<EFCorporateDal>().As<ICorporateDal>();

            builder.RegisterType<HcpUploadManager>().As<IHcpUploadService>();
            builder.RegisterType<EFHcpUploadDal>().As<IHcpUploadDal>();

            builder.RegisterType<HcpManager>().As<IHcpService>();
            builder.RegisterType<HcpHelper>().As<IHcpHelper>();

            builder.RegisterType<MailAddressManager>().As<IMailAddressService>();
            builder.RegisterType<EFMailAddressDal>().As<IMailAddressDal>();

            builder.RegisterType<MenuOperationClaimManager>().As<IMenuOperationClaimService>();
            builder.RegisterType<EFMenuOperationClaimDal>().As<IMenuOperationClaimDal>();

            builder.RegisterType<MenuManager>().As<IMenuService>();
            builder.RegisterType<EFMenuDal>().As<IMenuDal>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EFOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<PaymentRequestDetailManager>().As<IPaymentRequestDetailService>();
            builder.RegisterType<EFPaymentRequestDetailDal>().As<IPaymentRequestDetailDal>();

            builder.RegisterType<PaymentRequestManager>().As<IPaymentRequestService>();
            builder.RegisterType<EFPaymentRequestDal>().As<IPaymentRequestDal>();

            builder.RegisterType<PaymentRequestSummaryManager>().As<IPaymentRequestSummaryService>();
            builder.RegisterType<EFPaymentRequestSummaryDal>().As<IPaymentRequestSummaryDal>();

            builder.RegisterType<RoleOperationClaimManager>().As<IRoleOperationClaimService>();
            builder.RegisterType<EFRoleOperationClaimDal>().As<IRoleOperationClaimDal>();

            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<EFRoleDal>().As<IRoleDal>();

            builder.RegisterType<SftpDownloadManager>().As<ISftpDownloadService>();
            builder.RegisterType<EFSftpDownloadDal>().As<ISftpDownloadDal>();

            builder.RegisterType<SftpManager>().As<ISftpService>();
            builder.RegisterType<SftpDal>().As<ISftpDal>();

            builder.RegisterType<SmtpManager>().As<ISmtpService>();
            builder.RegisterType<SmtpHelper>().As<ISmtpHelper>();

            builder.RegisterType<TandemManager>().As<ITandemService>();
            builder.RegisterType<TandemDal>().As<ITandemDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EFUserDal>().As<IUserDal>();

            builder.RegisterType<UserRoleManager>().As<IUserRoleService>();
            builder.RegisterType<EFUserRoleDal>().As<IUserRoleDal>();

            builder.RegisterType<WebLogManager>().As<IWebLogService>();
            builder.RegisterType<EFWebLogDal>().As<IWebLogDal>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<HttpContextAccessorManager>().As<IHttpContextAccessorService>();

            builder.RegisterType<WindowManager>().As<IWindowService>();

            builder.RegisterType<Stopwatch>();

            var assembly =System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }







    }
}
