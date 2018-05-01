    $(".actionGroup").each(function(index) {
        var Group = $(this);
        var Tab = Group.find(".actionTab > label > input");
        var Panel = Group.find("> .actionPanel");
        function checkz(selector,reactor){
               selector.each(function() {
            var $this = $(this);
               if ($this.is(":checked")) {
                reactor.hide();
                reactor.filter("[data-id='" + $this.val() + "']").show();
            }
             });
        }
        checkz(selector=Tab,reactor=Panel);
        Tab.on("change",function() {
         checkz(selector=$(this),reactor=Panel);
        });
    });


// HFULL Code

function hFull(){
     var $mainh = $(".main").innerHeight() - 20;
    $(".h-full").css({"min-height":$mainh});
   }

$(window).on("load",function(){
    hFull();
});
$(window).on("resize",function(){

    hFull();

});

// HFULL Code


function sticky(){
       $(".sidebar .sidebar_menu").stick_in_parent({
        parent: ".main",
        offset_top: $(".navigation").height()
    }).on('sticky_kit:bottom', function(e) {
        $(this).parent().css('position', 'static');
    }).on('sticky_kit:unbottom', function(e) {
        $(this).parent().css('position', 'relative');
    }); 
        if ($(window).width() < 1920) {
        $(".sidebar").trigger("sticky_kit:detach");
    } else {
      
    }

}

sticky();