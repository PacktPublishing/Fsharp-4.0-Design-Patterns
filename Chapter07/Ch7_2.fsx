// https://msdn.microsoft.com/en-us/library/dd233232.aspx
// BEWARE: Does not work for test cases like Odd (even number) - recursion runs away!
let rec Even x = if x = 0 then true else Odd (x - 1)
and Odd x = if x = 1 then true else Even (x - 1)

// Prime number generator using mutual recursion
// The implementation is taken from my Stack Overflow answer at http://stackoverflow.com/a/9772027/917053
#nowarn "40"

let rec primes = 
    Seq.cache <| seq { yield 2; yield! Seq.unfold nextPrime 3 }
and nextPrime n =
    let next = n + if n%6 = 1 then 4 else 2 in
    if isPrime n then Some(n, next) else nextPrime next
and isPrime n =
    if n >= 2 then
        primes 
        |> Seq.tryFind (fun x -> n % x = 0 || x * x > n)
        |> fun x -> x.Value * x.Value > n
    else false

let problem010 () =
    primes
    |> Seq.takeWhile ((>) 2000000)
    |> (Seq.map int64 >> Seq.sum)
