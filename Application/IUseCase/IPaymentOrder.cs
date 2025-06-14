using Domain.Dtos;
using Domain.Entities;

namespace Application.ICaseUse;

public interface IPaymentOrder
{
    Task<PaymentGetRequestDomain> GeyDataPayment(int id);
    Task<Boolean> Payment(int id, PaymentCartDto card);
}