using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.ValidationRules.FluentValidation.SmtpValidators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Mail.Smtp.Abstract;
using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using Core.Utilities.Results.Abstract;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class SmtpManager : ISmtpService
    {
        private ISmtpHelper _smtpHelper;
        public SmtpManager(ISmtpHelper smtpHelper)
        {
            _smtpHelper = smtpHelper;
        }

        [SecurityAspect("Smtp.SendSmtpMail", Priority = 2)]
        [ValidationAspect(typeof(SmtpSendRequestDtoValidator), Priority = 3)]
        public async Task<IResult> SendSmtpMail(SmtpSendRequestDto smtpSendRequestDto)
        {
          return await _smtpHelper.SendSmtpMail(smtpSendRequestDto);        
        }
    }
}
