Option Explicit On 

Imports Microsoft.VisualBasic

'***************************************************************************
'This class holds a collection of group objects.  It provides methods for
'filling the collection with group objects based on the records in the
'groups table, inserting new groups and deleting groups.
'***************************************************************************

Public Class Groups

	'Inherit the ObjectCollection class
	Inherits ObjectCollection
	
	'Fills the collection with records from the Groups table
    Public Sub Fill(ByVal GroupNumbers As GroupNumbers, _
        ByVal CoverageTiers As CoverageTiers)

        Dim strSQL As String
        Dim strPayMode As String
        Dim strPlanID As String
        Dim strTaxID As String
        Dim intID As Integer
        Dim strGroup As String

        strSQL = "SELECT * " & _
            "FROM tblGroups "

        With objDataAccess
            'Fill the DataAccess object with records from the groups table
            .Fill(strSQL)

            'Loop through each record in the table
            While Not .EOF

                'Get the group properties from the table
                intID = .GetValue("intID")
                strPayMode = .GetValue("strPayMode")
                strPlanID = .GetValue("strPlanID")
                strTaxID = .GetValue("strTaxID")
                strGroup = .GetValue("strGroupName")

                'Add a new group object to the collection for each record in the table
                Me.BaseCollection.Add(New Group(intID, strGroup, strPayMode, strPlanID, strTaxID, GroupNumbers, CoverageTiers), CStr(intID))

                'Move to the next record
                .MoveNext()
            End While
        End With

    End Sub

    'Returns the Group object that that has the specified name
    Public Function Group(ByVal GroupName As String) As Group

        Dim objGroup As Group

        'Loop through each group in the collection
        For Each objGroup In Me.BaseCollection
            'Return the group with the matching name
            If objGroup.GroupName = GroupName Then
                Return objGroup
            End If
        Next

        Return Nothing

    End Function

    'Returns the group object that has the specified id
    Public Function Group(ByVal ID As Integer) As Group
        Return Me.BaseCollection(CStr(ID))
    End Function


    'Returns a boolean value indicating whether or not the specified group 
    'is in the collection
    Public Function InCollection(ByVal GroupName As String) As Boolean
        Return Not Group(GroupName) Is Nothing
    End Function

End Class