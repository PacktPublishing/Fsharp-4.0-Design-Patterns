#load "HugeNumber.fs"
hugeNumber |> (Seq.map (string >> int) >> Seq.windowed 5
>> Seq.map (Seq.reduce (*)) >> Seq.max
>> printfn "%s %d" "Functional solution:")
