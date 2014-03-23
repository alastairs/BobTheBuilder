BobTheBuilder
=============

Motivation
----------

One of the more immediately applicable recommendations from *Growing Object-Oriented Software, Guided by Tests (GOOS)* is to make use
of Test Data Builders, to allow you to construct test data with a fluent syntax, such as

````csharp
ACustomer()
	.WithGivenName("John")
	.WithFamilyName("Doe")
	.WithAddress(
		anAddress()
			.WithFirstLine("123 Main Street")
			.WithNoSecondLine()
			.WithCity("Nashville")
			.WithState("TN")
			.WithZip("55732")
			.Build())
	.Build();
````

This pattern is immensely powerful, but requires the writing of a lot of code for each type you wish to build. The aim of BobTheBuilder
is to provide a generic implementation of the Test Data Builder pattern from GOOS, using C#'s support for dynamic dispatch to intelligently
hydrate the destination object. 

Inspiration
-----------

The idea for this library was partially inspired by previous exposure to [Simple.Data](https://github.com/markrendle/Simple.Data).