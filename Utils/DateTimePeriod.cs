namespace Utils
{
  public class DateTimePeriod
  {
    public DateTimePeriod(){}
    public DateTimePeriod(long from, long to)
    {
      DateTimeFrom = from;
      DateTimeTo   = to  ;
    }
    public void Reset()
    {
      DateTimeFrom = 0;
      DateTimeTo   = 0;
    }

    public bool IsEmpty()
    {
      return DateTimeFrom == 0 && DateTimeTo == 0;
    }
    public long DateTimeFrom { get; set; }
    public long DateTimeTo { get; set; }

  }
}
