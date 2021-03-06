﻿using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using MongoDB.Bson;

namespace SmartHome.DAL.Transactions
{
    public class LoggingMongoDbQuery : MongoDbQueryDecorator
    {
        public LoggingMongoDbQuery(MongoDbQuery mongoDbQuery) : base(mongoDbQuery)
        {
        }

        public override void Execute()
        {
            // This impl can be changed in the future, for now it prints into console some logging information of the 
            // query being executed. Could be used for logging to external file or using some logging library
            String message = String.Format("{0} ({1}): Execute - {2}",
                DateTime.Now.ToString(CultureInfo.CurrentCulture),
                DecoratedMongoDbQuery.GetType().Name, DecoratedMongoDbQuery.ToString());
            File.AppendAllText(@"log.txt", message + Environment.NewLine);
            Console.WriteLine(message);
            DecoratedMongoDbQuery.Execute();
        }

        public override void Undo()
        {
            // This impl can be changed in the future, for now it prints into console some logging information of the 
            // query being executed. Could be used for logging to external file or using some logging library
            String message = String.Format("{0} ({1}): Undo - {2}", DateTime.Now.ToString(CultureInfo.CurrentCulture),
                DecoratedMongoDbQuery.GetType().Name, DecoratedMongoDbQuery.ToString());
            File.AppendAllText(@"log.txt", message + Environment.NewLine);
            Console.WriteLine(message);
            DecoratedMongoDbQuery.Undo();
        }

        public override string ToString()
        {
            return DecoratedMongoDbQuery.ToString();
        }
    }
}