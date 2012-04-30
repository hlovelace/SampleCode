<?php
    function getOptionValueFromSQL($sSelectSQL, $sValueField, $sNameField) {
       
        $db400 = new DB400();
        $optRS = $db400->run($sSelectSQL);
             
                 while( $row = $optRS->next() )  {
                     
                  $sOptValues  = $sOptValues . '<option value="'.  $row[ strtoupper($sValueField) ]   .'">' . $row[ strtoupper($sNameField) ]   . ' </option>';
                    
                 }
                                
               //set db400 to nothing
               $db400 = null;
               
              return $sOptValues;
    
    }
   
   function getOptionValueFromRS($sTableName, $sWhere, $sOrderBy, $sSelectField, $sSelectMatch) {
        
        $sSQL = "SELECT * FROM " . $sTableName . " WHERE " . $sWhere . " ORDER BY ". $sOrderBy;
        $gotMatch = "false";
        
        $db400 = new DB400();
        $optRS = $db400->run($sSQL);
             
                 while( $row = $optRS->next() )  {
                        $selText = "";
                     
                    $selectValue = trim($optRS->getValue($sSelectField));
                    $seltext = "";
                    if( $selectValue == $sSelectMatch ) {
                        $selText = " selected";
                        $gotMatch = 'true';
                    }
                                        
                    $sOptValues = $sOptValues . '<option value="'. $selectValue .'"'. $selText .'>'. $selectValue .'</option>';
                    
                    
                 }
                 
                 
                if( $gotMatch == 'false' ) {
                    $valText = $selectMatch;
                    $otherSel = " selected";
                   
                }
                
                 $sOptValues = $sOptValues . '<option value="Other" '. $otherSel .'>Other</option>';
                 
               //set db400 to nothing
              return $sOptValues;
    
    }
    
   function getDates($DateSelected) {
    
    $arrDates = array();
    
    $sDay = date("l", strtotime($DateSelected));   
    
        while($sDay != "Sunday") {          
          $DateSelected = date("Ymd", strtotime($DateSelected . " - 1 day"));  
          $sDay = date("l", strtotime($DateSelected));   
        }
       
     for ($j = 0; ($j < 7); $j++) {           
        $sSelectDate = date("m/d/y", strtotime($DateSelected . " + ". $j ." day"));  
        $arrDates[$j] = $sSelectDate;  
    } 

   return $arrDates; 
}   

function getLastMonday($date) {
        
        $dayNum = date("w", strtotime($date));
        
        while($dayNum != 1)  {
     
            $date = date("m/d/Y", strtotime(date("Y-m-d", strtotime($date)) . " -1 day"));
            $dayNum = date("w", strtotime($date));
        }    
       
       return $date;  
       
    }
    
?>
