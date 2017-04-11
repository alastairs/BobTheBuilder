BobTheBuilder
=============

[![Build status](https://ci.appveyor.com/api/projects/status/9m6e571bsu9082mi/branch/master?svg=true)](https://ci.appveyor.com/project/alastairs/bobthebuilder/branch/master)

Motivation
----------

One of the more immediately applicable recommendations from *Growing Object-Oriented Software, Guided by Tests (GOOS)* is to make use of Test Data Builders, to allow you to construct test data with a fluent syntax, such as

````csharp
ACustomer()
	.WithGivenName("John")
	.WithFamilyName("Doe")
	.WithAddress(
		AnAddress()
			.WithFirstLine("123 Main Street")
			.WithNoSecondLine()
			.WithCity("Nashville")
			.WithState("TN")
			.WithZip("55732")
			.Build())
	.Build();
````

This pattern is immensely powerful, but requires the writing of a lot of code for each type you wish to build. The aim of BobTheBuilder is to provide a generic implementation of the Test Data Builder pattern from GOOS, using C#'s support for dynamic dispatch to intelligently hydrate the destination object. For example, using the same hypothetical types as above:

````csharp
Customer customer = A.BuilderFor<Customer>()
                        .WithGivenName("John")
                            .WithFamilyName("Doe")
                            .WithAddress(
                                A.BuilderFor<Address>()
                                    .WithFirstLine("123 Main Street")
                                    .WithCity("Nashville")
                                    .WithState("TN"));
````

Note that a dynamic builder can be implicitly cast to the type it builds and the `Build()` method will be automatically invoked. This saves you a bit of typing if desired and reduces noise, but requires you to explicitly state your variable types; it comes into its own when nesting builders, though, as in the example above. 

Task List
---------

 - ~~Add support for readonly properties via the constructor~~
 - Add support for objects with constructor dependencies
 - ~~Add support for complex types~~ (**Complete**)
 - ~~Add support for named arguments syntax (`.With(stringProperty: "new value")`)~~ (**Complete**)
 - Get a better name...

Please [request features and report bugs](https://github.com/alastairs/BobTheBuilder/issues), or better still [fork me](https://github.com/alastairs/BobTheBuilder/fork) and [send me a pull request](https://github.com/alastairs/BobTheBuilder/compare/). 

Inspiration
-----------

The idea for this library was partially inspired by previous exposure to [Simple.Data](https://github.com/markrendle/Simple.Data), and the number of Test Data Builders I have written in the last twelve months. Thanks Steve and Nat for such an elegantly simple, and yet extremely powerful, piece of advice.
