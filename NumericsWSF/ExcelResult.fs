module NumericsWSF.ExcelResult

open ExcelDna.Integration

type ExcelResult =
    | Value of float
    | Error of ExcelError

let valueError = Error ExcelError.ExcelErrorValue

// Because ExcelDNA doesn't accept union types, we must
// return obj to it
let toObject r =
    match r with
    | Value v -> v :> obj
    | Error e -> e :> obj

let toString (r:ExcelResult) =
    match r with
    | Value v -> sprintf "%f" v
    | Error _ -> "error"