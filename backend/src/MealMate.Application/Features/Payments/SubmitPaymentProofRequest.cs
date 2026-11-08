using Microsoft.AspNetCore.Http;

namespace MealMate.Application.Features.Payments;

public sealed record SubmitPaymentProofRequest(IFormFile File);