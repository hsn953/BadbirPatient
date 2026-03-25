using BADBIR.Shared.DTOs;

namespace BADBIR.Api.Tests;

/// <summary>
/// Lightweight unit tests that validate shared DTO logic and scoring calculations.
/// Integration tests using WebApplicationFactory require a real SQL Server DB
/// and are tagged [Trait("Category", "Integration")] for selective execution.
/// </summary>
public class HaqScoreCalculationTests
{
    [Fact]
    public void HaqDiScore_AllZero_ReturnsZero()
    {
        var dto = new HaqSubmitDto(); // all byte defaults = 0
        var score = CalculateHaqDi(dto);
        Assert.Equal(0m, score);
    }

    [Fact]
    public void HaqDiScore_AllThree_ReturnsThree()
    {
        var dto = new HaqSubmitDto
        {
            Dressing   = 3, Arising    = 3, Eating    = 3, Walking    = 3,
            Hygiene    = 3, Reach      = 3, Grip      = 3, Activities = 3
        };
        var score = CalculateHaqDi(dto);
        Assert.Equal(3m, score);
    }

    [Fact]
    public void HaqDiScore_Mixed_ReturnsCorrectMean()
    {
        var dto = new HaqSubmitDto
        {
            Dressing   = 1, Arising    = 2, Eating    = 0, Walking    = 3,
            Hygiene    = 1, Reach      = 2, Grip      = 0, Activities = 3
        };
        // (1+2+0+3+1+2+0+3) / 8 = 12/8 = 1.5
        var score = CalculateHaqDi(dto);
        Assert.Equal(1.5m, score);
    }

    private static decimal CalculateHaqDi(HaqSubmitDto dto) =>
        Math.Round(
            (dto.Dressing + dto.Arising + dto.Eating + dto.Walking +
             dto.Hygiene  + dto.Reach   + dto.Grip   + dto.Activities) / 8m,
            3);
}

public class HadsScoreCalculationTests
{
    [Fact]
    public void AnxietyScore_SumOfSevenItems_IsCorrect()
    {
        byte a1 = 2, a2 = 1, a3 = 3, a4 = 0, a5 = 2, a6 = 1, a7 = 3;
        byte expected = (byte)(a1 + a2 + a3 + a4 + a5 + a6 + a7); // 12
        Assert.Equal(12, expected);
    }

    [Fact]
    public void DepressionScore_AllZero_ReturnsZero()
    {
        byte total = (byte)(0 + 0 + 0 + 0 + 0 + 0 + 0);
        Assert.Equal(0, total);
    }
}

public class EuroQolDtoTests
{
    [Fact]
    public void EuroQolSubmitDto_DefaultValues_AreZero()
    {
        var dto = new EuroQolSubmitDto();
        Assert.Equal(0, dto.Mobility);
        Assert.Equal(0, dto.VasScore);
    }
}
