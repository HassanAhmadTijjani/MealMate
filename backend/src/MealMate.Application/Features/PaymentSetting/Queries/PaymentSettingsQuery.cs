using MealMate.Application.Common.Models.PaymentSetting;
using MediatR;

namespace MealMate.Application.Features.PaymentSetting.Queries;

public sealed record PaymentSettingsQuery : IRequest<PaymentSettingsResponse?>;