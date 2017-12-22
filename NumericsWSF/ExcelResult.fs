module NumericsWSF.ExcelResult

open ExcelDna.Integration

type ExcelResult =
    | Value of float
    | Text of string
    | Error of ExcelError
    | ComplexVal of ExcelComplex.ExcelComplex

let valueError = Error ExcelError.ExcelErrorValue

// Because ExcelDNA doesn't accept union types, we must
// return obj to it
let toObject r =
    match r with
    | Value v -> v :> obj
    | Error e -> e :> obj
    | Text t -> t :> obj
    | ComplexVal z -> ExcelComplex.toObject z

let toString (r:ExcelResult) =
    match r with
    | Value v -> sprintf "%f" v
    | Error _ -> "error"