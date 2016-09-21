﻿using BobTheBuilder.Activation;
using BobTheBuilder.ArgumentStore;
using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Syntax;
using Activator = BobTheBuilder.Activation.Activator;

namespace BobTheBuilder
{
    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            var argumentStore = new InMemoryArgumentStore();
            return
                new DynamicBuilder<T>(
                    new CompositeParser(
                        new NamedArgumentsSyntaxParser(argumentStore),
                        new MethodSyntaxParser(argumentStore)),
                    new Activator(
                        new MissingArgumentsQuery(argumentStore),
                        new BuilderArgumentEvaluator(
                            new ConstructorArgumentsQuery(argumentStore)),
                        new PropertyValuesQuery(argumentStore)));
        }
    }
}