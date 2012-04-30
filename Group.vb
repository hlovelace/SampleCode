Option Explicit On 

Imports Microsoft.VisualBasic

'***************************************************************************
'This class represents a Group and corresponds to a record 
'in the tblGroups table. It provides a method for updating the
'corresponding group record in the tblGroups table.
'***************************************************************************

Public Class Group

	dim intID as integer
    Dim strGroupName As String
    Dim strPayMode As String
    Dim strPlanID As String
    Dim strTaxID As String
    Dim colGroupNumbers As New GroupNumbers
    Dim colCoverageTiers As New CoverageTiers
	
	'Creates a new Group object and sets the object's properties
    Public Sub New(ByVal ID As Integer, _
        ByVal GroupName As String, _
        ByVal PayMode As String, _
        ByVal PlanID As String, _
        ByVal TaxID As String, _
        ByVal GlobalGroupNumbers As GroupNumbers)

        'Set the object properties
        intID = ID
        strGroupName = GroupName
        strPayMode = PayMode
        strPlanID = strPlanID
        strTaxID = TaxID

        'Fill the group numbers collection
        InitializeGroupNumbers(GlobalGroupNumbers)

    End Sub

    'Creates a new Group object and sets the object's properties (except for the group numbers collection)
    Public Sub New(ByVal ID As Integer, _
        ByVal GroupName As String, _
        ByVal PayMode As String, _
        ByVal PlanID As String, _
        ByVal TaxID As String)

        'Set the object properties
        intID = ID
        strGroupName = GroupName
        strPayMode = PayMode
        strPlanID = strPlanID
        strTaxID = TaxID

    End Sub

    'Creates a new Group object and sets the object's properties
    Public Sub New(ByVal ID As Integer, _
        ByVal GroupName As String, _
        ByVal PayMode As String, _
        ByVal PlanID As String, _
        ByVal TaxID As String, _
        ByVal GlobalGroupNumbers As GroupNumbers, _
        ByVal GlobalCoverageTiers As CoverageTiers)

        'Set the object properties
        intID = ID
        strGroupName = GroupName
        strPayMode = PayMode
        strPlanID = PlanID
        strTaxID = TaxID

        'Fill the group numbers collection
        InitializeGroupNumbers(GlobalGroupNumbers)

        'Fill the Coverage Tiers collection
        InitializeCoverageTiers(GlobalCoverageTiers)

    End Sub

    'Initializes the Coverage Tiers collection for this group object
    Private Sub InitializeCoverageTiers(ByVal GlobalCoverageTiers As CoverageTiers)

        Dim objCoverageTier As CoverageTier

        'Loop through each number in the global group numbers collection
        For Each objCoverageTier In GlobalCoverageTiers.BaseCollection
            If objCoverageTier.GroupID = Me.ID Then
                'If the group id of the group number object matches this 
                'group's id, add the group number object to the group numbers
                'collection
                Me.CoverageTiers.Add(objCoverageTier)
            End If
        Next

    End Sub

    'Initializes the group numbers collection for this group object
    Private Sub InitializeGroupNumbers(ByVal GlobalGroupNumbers As GroupNumbers)

        Dim objGroupNumber As GroupNumber

        'Loop through each number in the global group numbers collection
        For Each objGroupNumber In GlobalGroupNumbers.BaseCollection
            If objGroupNumber.GroupID = Me.ID Then
                'If the group id of the group number object matches this 
                'group's id, add the group number object to the group numbers
                'collection
                Me.GroupNumbers.Add(objGroupNumber, objGroupNumber.GroupNumber)
            End If
        Next

    End Sub

    'Updates the object's group name property and the 
    'corresponding record in the tblGroups table
    Public Sub SetGroupName(ByVal NewGroupName As String)

        Dim strCommandText As String

        'Set the command text to update the record
        strCommandText = "UPDATE tblGroups " & _
         "SET strGroupName = """ & NewGroupName & """ " & _
         "WHERE intID = " & Me.ID

        'Execute the command
        objDataAccess.RunCommand(strCommandText)

        'Update the group name property
        strGroupName = NewGroupName

    End Sub


    'Returns the group's ID (numeric key)
    Public ReadOnly Property ID() As Integer
        Get
            Return intID
        End Get
    End Property

    'Returns the Group Name
    Public ReadOnly Property GroupName() As String
        Get
            Return strGroupName
        End Get
    End Property

    'Returns the Pay Mode
    Public ReadOnly Property PayMode() As String
        Get
            Return strPayMode
        End Get
    End Property

    'Returns the Plan ID
    Public ReadOnly Property PlanID() As String
        Get
            Return strPlanID
        End Get
    End Property

    'Returns the Tax ID
    Public ReadOnly Property TaxID() As String
        Get
            Return strTaxID
        End Get
    End Property

    'Returns the GroupNumbers collection for this group
    Public ReadOnly Property GroupNumbers() As GroupNumbers
        Get
            Return colGroupNumbers
        End Get
    End Property

    'Returns the CoverageTiers collection for this group
    Public ReadOnly Property CoverageTiers() As CoverageTiers
        Get
            Return colCoverageTiers
        End Get
    End Property



End Class