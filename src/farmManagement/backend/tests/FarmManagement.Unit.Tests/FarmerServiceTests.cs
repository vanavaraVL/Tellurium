using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FarmManagement.Dal.Entities;
using FarmManagement.Dal.Repositories;
using FarmManagement.Models.Dto;
using FarmManagement.Services;
using Moq;
using NUnit.Framework;
using System.Text;

namespace FarmManagement.Unit.Tests;

public class FarmerServiceTests
{
    [Test, CustomAutoData]
    public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(FarmerService).GetConstructors());
    }

    [Test, CustomAutoData]
    public async Task Get_animal_list_should_pass([Frozen] IAnimalRepository repository, FarmerService sut, IFixture fixture)
    {
        // ARRANGE
        var entityList = fixture.CreateMany<AnimalEntity>(10).ToList();

        Mock.Get(repository)
            .Setup(r => r.GetAsync(It.IsAny<Func<AnimalEntity, bool>?>()))
            .ReturnsAsync(entityList);

        // ACT
        var result = await sut.GetAll();

        // ASSERTS
        Assert.That(entityList.Count, Is.EqualTo(result.ResultItem.Count));
    }

    [Test, CustomAutoData]
    public async Task Create_animal_should_pass([Frozen] IAnimalRepository repository, FarmerService sut, AnimalDto entityDto)
    {
        // ARRANGE
        Mock.Get(repository)
            .Setup(r => r.AddAsync(It.IsAny<AnimalEntity>()))
            .ReturnsAsync(new AnimalEntity() { Id = Guid.NewGuid(), Name = entityDto.Name });

        // ACT
        var result = await sut.CreateNewItem(entityDto);

        // ASSERTS
        Assert.That(result.ResultItem.Name, Is.EqualTo(entityDto.Name));
    }

    [Test, CustomAutoData]
    public async Task Edit_animal_should_pass([Frozen] IAnimalRepository repository, FarmerService sut, AnimalDto entityDto)
    {
        // ARRANGE
        Mock.Get(repository)
            .Setup(r => r.UpdateAsync(It.IsAny<AnimalEntity>(), It.IsAny<string>()))
            .ReturnsAsync(new AnimalEntity() { Id = Guid.NewGuid(), Name = entityDto.Name });

        // ACT
        var result = await sut.EditItem(entityDto, Convert.ToBase64String(Encoding.UTF8.GetBytes(entityDto.Name)));

        // ASSERTS
        Assert.That(result.ResultItem.Name, Is.EqualTo(entityDto.Name));
    }
}