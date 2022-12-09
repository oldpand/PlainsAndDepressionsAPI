using MediatR;
using PlainsAndDepressions.Services.Results;

namespace PlainsAndDepressions.Services.Commands;

public class MeadowProcessCommand : IRequest<Result>
{
    public MeadowProcessCommand(int[][] meadow)
    {
        Meadow = meadow;
    }

    public int[][] Meadow { get; }
}
