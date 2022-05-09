namespace HubooTechnicalExercise
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string PrimaryColour { get; set; }
        public IEnumerable<MotTest> MotTests { get; set; }

        public int GetNumberOfFailedMotTests()
        {
            var failedResultText = "FAILED";
            return MotTests.Where(t => failedResultText.Equals(t.testResult.ToUpper())).ToList().Count; 
        }

        //Although the data comes back in order it might not always be the case
        public DateTime GetNextMotDueDate() => MotTests.OrderByDescending(t => t.ExpiryDate).First().ExpiryDate.Date;
    }
}