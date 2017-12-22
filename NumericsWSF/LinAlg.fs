module NumericsWSF.LinAlg

open ExcelDna.Integration
open ExcelDna.Documentation
open MathNet.Numerics.LinearAlgebra
open Utils

[<ExcelFunctionDoc(
    Name="NUM.LINALG.SOLVE",
    Category="Linear Algebra",
    Description="Solve the system of linear equations Ax=b for x using QR factorization.",
    Remarks="Based on MathNet.Numerics.Solve"
)>]
let solve
        ([<ExcelArgument(Description="Matrix A (size NxN)")>]
        A:float[,])

        ([<ExcelArgument(Description="Vector b (length N)")>]
        b:float[]) =

    let inputValid =
        // A is a square matrix
        Array2D.length1 A = Array2D.length2 A
        // Length of b is equal to size of A
        && Array2D.length1 A = Array.length b

    match inputValid with
    | false -> Array.init (Array.length b) (fun _ -> ExcelResult.valueError)
    | true ->
        let Amat = CreateMatrix.DenseOfArray A
        let xv = CreateVector.DenseOfArray b
        Amat.Solve(xv).AsArray() |> Array.map ExcelResult.Value

    |> Array.map ExcelResult.toObject
    |> packForCaller