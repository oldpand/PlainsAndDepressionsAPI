using MediatR;
using PlainsAndDepressions.Contracts.Models;
using PlainsAndDepressions.Services.Commands;
using PlainsAndDepressions.Services.Results;
using PlainsAndDepressions.Services.Services;

namespace PlainsAndDepressions.Services.Handlers;

public class ProcessCommandHandler : IRequestHandler<MeadowProcessCommand, Result>
{
    private readonly IRabbitMqService _rabbitMqService = null!;

    public ProcessCommandHandler(IRabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public Task<Result> Handle(MeadowProcessCommand request, CancellationToken cancellationToken)
    {
        var depressions = FindDepressions(request.Meadow);

        var packId = Guid.NewGuid();

        var message = new PutDepressionsRequest
        {
            PackId = packId,
            Pack = depressions
        };

        //отправить в RabbitMQ
        _rabbitMqService.SendMessage(message);

        return Task.FromResult(new Result(packId));
    }

    private static IOrderedEnumerable<Depression> FindDepressions(int[][] arr)
    {
        //max teoretical length 
        int maxLength = arr.Max(i => i.Length);

        var depressions = new List<Depression>();
        //TO DO логически не правильно создавать объект, пака в нем не появилась необходимость 
        var depression = new Depression();

        for (int i = 0; i < maxLength; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                //detect empty place of array
                if (arr[j].Length - 1 < i)
                {
                    if (depression.InProc)
                    {
                        depressions.Add(depression);
                    }
                    depression.InProc = false;
                    continue;
                }

                if (arr[j][i] == 0)
                {
                    if (depression.InProc)
                    {
                        depression++;
                    }
                    else
                    {
                        depression = new Depression();
                        depression++;
                    }

                    if (j == arr.Length - 1)
                    {
                        depression.InProc = false;
                        depressions.Add(depression);
                    }
                }
                else
                {
                    if (depression.InProc)
                    {
                        depression.InProc = false;
                        depressions.Add(depression);
                    }
                }
            }
        }

        return depressions.OrderBy(d => d.Size);
    }
}
