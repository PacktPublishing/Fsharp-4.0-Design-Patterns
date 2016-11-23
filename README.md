#F# 4.0 Design Patterns
This is the code repository for [F# 4.0 Design Patterns](https://www.packtpub.com/application-development/f-40-design-patterns?utm_source=github&utm_campaign=9781785884726&utm_medium=repository) By Packt. It contains all the supporting project files necessary to work through the book from start to finish.

##Instructions and Navigation
All of the code is organized into folders. Each folder starts with number followed by the application name. For example, Chapter02.

You will see code something similar to the following:

```
type IMyInterface =
    abstract member DoIt: unit -> unit
    
type MyImpl() =
    interface IMyInterface with
        member __.DoIt() = printfn "Did it!"
        
MyImpl().DoIt() // Error: member 'DoIt' is not defined

(MyImpl() :> IMyInterface).DoIt()

// ... but
let doit (doer: IMyInterface) =
    doer.DoIt()

doit (MyImpl())

```

Software and Hardware List



##Related Haskell Products:
* [Learning F# Functional Data Structures and Algorithms](https://www.packtpub.com/application-development/learning-f-functional-data-structures-and-algorithms?utm_source=github&utm_campaign=9781783558476&utm_medium=repository)
* [F# for Machine Learning Essentials](https://www.packtpub.com/big-data-and-business-intelligence/f-machine-learning?utm_source=github&utm_campaign=9781783989348&utm_medium=repository)
* [F# 4.0 Programming Cookbook](https://www.packtpub.com/application-development/f-40-programming-cookbook?utm_source=github&utm_campaign=9781786468369&utm_medium=repository)






### Suggestions and Feedback
[Click here] (https://docs.google.com/forms/d/e/1FAIpQLSe5qwunkGf6PUvzPirPDtuy1Du5Rlzew23UBp2S-P3wB-GcwQ/viewform) if you have any feedback or suggestions.
