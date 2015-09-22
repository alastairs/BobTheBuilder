Remaining items from Corey Haines' code review:

~~https://softwarecraftsmanship.slack.com/archives/code-review/p1442490996000286~~
> ~~Oh, there is a MemberNameAndValue type, this piques my interest. If this is true, then why are the arguments here: https://github.com/alastairs/BobTheBuilder/compare/refactor#diff-89fedd88d3fd99c3f33f0985091d9e47R7~~
> ~~string/object~~
> ~~```void SetMemberNameAndValue(string name, object value);```~~
> ~~and not~~
> ~~```void Set(MemberNameAndValue newValues);```~~

https://softwarecraftsmanship.slack.com/archives/code-review/p1442491272000292
> Ah, here: https://github.com/alastairs/BobTheBuilder/compare/refactor#diff-b52b567330ab0d5f05f260078ab2fbbfR26
> I think I see this. The looping and removal seems like it should be in ArgumentStore, something like
> ```Remove(IEnumberable names)```
> Then, this method would simplify dramatically, as you would just determine the list and pass it to the argument store.
> Also, you could probably move the conversion call `ToList` to the return statement, depending on what `Remove` accepted

https://softwarecraftsmanship.slack.com/archives/code-review/p1442490996000286
> Mostly I look at this method: https://github.com/alastairs/BobTheBuilder/compare/refactor#diff-b52b567330ab0d5f05f260078ab2fbbfR22
and it seems SO BIG!!!! It seems like its job is to determine the list of constructor arguments for a type’s first(?) constructor. Then, suddenly, it is looping. YIKES!

https://softwarecraftsmanship.slack.com/archives/code-review/p1442491463000297
> You could end it up with something like
```var parameterNames = destinationType.GetConstructors().Single().GetParameters().Select(p => p.Name.ToPascalCase());
var constructorArguments = argumentStore.GetAllStoredMembers().Where(member => parameterNames.Contains(member.Name)).ToList();
argumentStore.Remove(constructorArgument.Select(m => m.Name));
return constructorArguments;
```
This reads better to me.
As always, everything is prefaced with IMNSHO.

https://softwarecraftsmanship.slack.com/archives/code-review/p1442491665000298
> So,
MissingArgumentsQuery https://github.com/alastairs/BobTheBuilder/compare/refactor#diff-f9f922159e720c7a2534c5e31ad028bcR7
and
PropertyValuesQuery https://github.com/alastairs/BobTheBuilder/compare/refactor#diff-2cb3fcfc4620d1b65042a22f0b79c70eR7
are exactly the same. It appears that they are separate so that we can have a concrete name. But, yet, these are internal, so the external world will only see them as IArgumentStoreQuery, if they even make it out.
So, is this a utility that is begging to be consolidated and named differently?
