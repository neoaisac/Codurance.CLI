using Codurance.Business.Model.Repositories;
using Codurance.Repository.SQLCE.Base;

namespace Codurance.Repository.SQLCE
{
	public class CoduranceRepositoryContext : SelfDestructingSQLCERepositoryContextBase, ICoduranceRepositoryContext
	{
	}
}