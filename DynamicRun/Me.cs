namespace DynamicRun
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class Me
    {
        public static void Notify(IEnumerable<Match> matches)
        {
            var outputPath = @"C:\code\cset\mymatches_chrisb.txt";

            var builder = new StringBuilder();

            foreach (var match in matches)
            {
                builder.AppendLine(match.Candidate.FirstName + " " + match.Candidate.LastName);
                builder.AppendLine(match.Opportunity.Company);
                builder.AppendLine("************************");
            }


            File.WriteAllText(outputPath, builder.ToString());
        }
    }
}
