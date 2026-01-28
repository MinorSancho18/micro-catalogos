using FluentAssertions;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories;
using ExamenProcomerBackend.Application.Interfaces;
using Xunit;

namespace ExamenProcomerBackend.Infrastructure.Tests
{
    public class RepositoriesInstantiationTests
    {
        [Fact]
        public void CanInstantiate_CategoriaVehiculoRepositories_WithDapperContext()
        {
            var ctx = new DapperContext("Server=(local);Database=Fake;User Id=sa;Password=Passw0rd;");

            var cmdRepo = new CategoriaVehiculoCommandRepository(ctx);
            var qryRepo = new CategoriaVehiculoQueryRepository(ctx);

            cmdRepo.Should().NotBeNull();
            qryRepo.Should().NotBeNull();

            cmdRepo.Should().BeAssignableTo<ICategoriaVehiculoCommandRepository>();
            qryRepo.Should().BeAssignableTo<ICategoriaVehiculoQueryRepository>();
        }
    }
}
