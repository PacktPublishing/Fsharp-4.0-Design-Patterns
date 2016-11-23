// Lazy Evaluation
let twoByTwo  = lazy (let r = 2*2 in
                      printfn "Everybody knows that 2*2=%d" r; r)
twoByTwo.Force()
twoByTwo.Force()

