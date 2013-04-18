'    Miscelaneous Math Functions for RELAP
'    Copyright 2008 Daniel Wagner O. de Medeiros
'
'    This file is part of RELAP.
'
'    RELAP is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    RELAP is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with RELAP.  If not, see <http://www.gnu.org/licenses/>.

Namespace RELAP.MathEx

    Public Class Common

        Shared Function CopyToVector(ByVal arr As ArrayList, ByVal index As Integer) As Double()

            Dim i As Integer = 0
            Dim values(arr.Count - 1) As Double

            For i = 0 To arr.Count - 1
                values(i) = arr(i)(index)
            Next

            Return values

        End Function

        Shared Function Max(ByVal Vv As Object, ByVal Vz As Object)

            Dim n = UBound(Vv)
            Dim mx As Double

            If n >= 1 Then
                Dim i As Integer = 1

                'Do
                '    mx = Vv(i - 1)
                '    i += 1
                'Loop Until i = n + 1

                mx = Vv(i - 1)
                i = 0
                Do
                    If Vv(i) > mx And Vz(i) <> 0 Then
                        mx = Vv(i)
                    End If
                    i += 1
                Loop Until i = n + 1

                Return mx
            Else
                Return Vv(0)
            End If

        End Function

        Shared Function Max(ByVal Vv As Object)

            Dim n = UBound(Vv)
            Dim mx As Double

            If n >= 1 Then
                Dim i As Integer = 1

                'Do
                '    mx = Vv(i - 1)
                '    i += 1
                'Loop Until i = n + 1

                mx = Vv(i - 1)
                i = 0
                Do
                    If Vv(i) > mx Then
                        mx = Vv(i)
                    End If
                    i += 1
                Loop Until i = n + 1

                Return mx
            Else
                Return Vv(0)
            End If

        End Function

        Shared Function Min(ByVal Vv As Object, ByVal Vz As Object)

            Dim n = UBound(Vv)
            Dim mx As Double

            If n >= 1 Then
                Dim i As Integer = 1

                'Do
                '    mx = Vv(i - 1)
                '    i += 1
                'Loop Until i = n + 1

                mx = Vv(i - 1)
                i = 0
                Do
                    If Vv(i) < mx And Vz(i) <> 0 Then
                        mx = Vv(i)
                    End If
                    i += 1
                Loop Until i = n + 1

                Return mx
            Else
                Return Vv(0)
            End If

        End Function

        Shared Function Min(ByVal Vv As Object)

            Dim n = UBound(Vv)
            Dim mx As Double

            If n >= 1 Then
                Dim i As Integer = 1

                Do
                    If Vv(i - 1) <> 0 Then
                        mx = Vv(i - 1)
                        Exit Do
                    End If
                    i += 1
                Loop Until i = n + 2

                i = i - 1
                Do
                    If Vv(i) < mx Then
                        mx = Vv(i)
                    End If
                    i += 1
                Loop Until i = n + 1

                Return mx
            Else
                Return Vv(0)
            End If

        End Function

        Shared Function Sum(ByVal vx As Object)

            Dim n = UBound(vx)
            Dim i As Integer
            Dim sumv As Double

            sumv = 0
            For i = 0 To n
                sumv += vx(i)
            Next

            Return sumv

        End Function

        Shared Function AbsSum(ByVal vx As Object)

            Dim n = UBound(vx)
            Dim i As Integer
            Dim sumv As Double

            sumv = 0
            For i = 0 To n
                sumv += Math.Abs(vx(i))
            Next

            Return sumv

        End Function

    End Class

End Namespace