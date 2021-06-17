using Bogus;
using System;
using System.Collections.Generic;
using VxTel.Domain.Entities;

namespace VxTel.Application.Tests
{
    public partial class TestsFixtures
    {
        public Plan GenerateNewPlan() =>
            new Plan(Guid.NewGuid(), "FaleMais60", 60, DateTime.Now, null);

        public IReadOnlyList<Plan> GenerateNewPlans()
        {

            var fakerPlans = new Faker<Plan>()
                .CustomInstantiator(f => new Plan(
                    Guid.NewGuid(),
                    "FaleMais" + f.Random.Number(0, 999),
                    f.Random.Number(0, 999),
                    DateTime.Now,
                    null
                    ));
            return fakerPlans.Generate(5);
        }

        public Price GenerateNewPrice() =>
            new Price(Guid.NewGuid(), "011", "017", 1.7, DateTime.Now, null);

        public IReadOnlyList<Price> GenerateNewPrices()
        {

            var fakerPrices = new Faker<Price>()
                .CustomInstantiator(f => new Price(
                    Guid.NewGuid(),
                    "0" + f.Random.Number(11, 99),
                    "0" + f.Random.Number(11, 99),
                    f.Random.Double(1, 3),
                    DateTime.Now,
                    null
                    ));
            return fakerPrices.Generate(5);
        }
    }
}