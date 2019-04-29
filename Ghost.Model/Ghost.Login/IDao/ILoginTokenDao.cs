using Ghost.CRUD.IDao;
using Ghost.Login.Domain;

namespace Ghost.Login.IDao
{
    public interface ILoginTokenDao : IDaoBase<LoginToken, long>
    {
    }
}
