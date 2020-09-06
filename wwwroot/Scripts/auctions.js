
function deleteAuctionConfirm(id)
{
    if (confirm("Are you sure you want permanently delete this auction?")) {
        var verificationToken = $("input[name='__RequestVerificationToken']").val ( ); 
        $.ajax ({  
            type: "POST", 
            url: "/Auction/DeleteAuction", 
            data: { 
                "id": parseInt(id), 
                "__RequestVerificationToken" : verificationToken 
            },
            success: function ( response ) {           
                alert ( "You deleted your auction successfully!" );
                location.reload(); 
            },
            error: function ( response ) {
                alert ( "Error, you can only delete auction in Draft state!" ); 
            }
        });
        
      }
}





