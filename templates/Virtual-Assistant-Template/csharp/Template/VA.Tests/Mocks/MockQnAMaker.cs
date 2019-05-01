﻿using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Mocks
{
    public class MockQnAMaker : ITelemetryQnAMaker
    {
        public MockQnAMaker(QueryResult[] defaultAnswer)
        {
            TestAnswers = new Dictionary<string, QueryResult[]>();
            DefaultAnswer = defaultAnswer;
        }

        private Dictionary<string, QueryResult[]> TestAnswers { get; set; }

        private QueryResult[] DefaultAnswer { get; set; }

        public bool LogPersonalInformation { get; set; } = false;

        public IBotTelemetryClient TelemetryClient { get; set; } = new NullBotTelemetryClient();

        public void RegisterAnswers(Dictionary<string, QueryResult[]> utterances)
        {
            foreach (var utterance in utterances)
            {
                TestAnswers.Add(utterance.Key, utterance.Value);
            }
        }

        public Task<QueryResult[]> GetAnswersAsync(ITurnContext context, QnAMakerOptions options = null)
        {
            var text = context.Activity.Text;

            var mockResult = TestAnswers.GetValueOrDefault(text, DefaultAnswer);
            return Task.FromResult(mockResult);
        }

        public Task<QueryResult[]> GetAnswersAsync(ITurnContext turnContext, QnAMakerOptions options, Dictionary<string, string> telemetryProperties, Dictionary<string, double> telemetryMetrics = null)
        {
            var text = turnContext.Activity.Text;

            var mockResult = TestAnswers.GetValueOrDefault(text, DefaultAnswer);
            return Task.FromResult(mockResult);
        }
    }
}