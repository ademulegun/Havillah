namespace Havillah.Core.Domain;

public class MeasuringUnit: BaseEntity<int>
{
    public MeasuringUnit()
    {
            
    }
    public string Unit { get; set; }
    public DateTime Created { get; set; }
}