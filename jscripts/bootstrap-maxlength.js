$('table tbody tr').keyup(function () {

    iRowIndex = $(this).closest("tr").prevAll("tr").length;
    console.log("GridViewTrueAns_LBL_" + (iRowIndex - 1));
    

});


!function ($) {
    $.fn.maxlength = function () {
    $(this).each(function() {
        var max = $(this).attr('MaxLength');
        //$("#GridViewTrueAns_LBL_" + (iRowIndex - 1)).removeAttr("Visible");
      

        iRowIndex = $(this).closest("tr").prevAll("tr").length;
        console.log("GridViewTrueAns_LBL_" + (iRowIndex - 1));
       // $("#GridViewTrueAns_LBL_" + (iRowIndex - 1)).removeClass("hidden");

      if (max <= 0 || max === undefined) {
        throw new Error('maxlength attribute must be defined and greater than 0');
      }

      //if (!$(this).parent().hasClass('input-group')) {
      //  $(this).wrap("<div class=\"input-group\"></div>");
      //}
      //$(this).after("<span class=\"input-group-addon maxlength\"></span>");

      $(this).bind('input', function(e) {
          var max = $(this).attr('MaxLength');
        var val = $(this).val();
        var cur = 0;

        if (val) {
          cur = val.length;
        }

        var left = cur;

          if (cur == max) {
                $("#MAXChartyp").removeClass("hidden")
            
              
              $(".LBL").addClass("countCharGreen");

          } else {
              $("#MAXChartyp").addClass("hidden")
              $("#GridViewFalseAns_MAXChartyp_" + (iRowIndex - 1)).addClass("hidden")
              $(".LBL").removeClass("countCharRed");
              $(".LBL").addClass("countCharGreen");

          }

          if (cur == 0) {
              $(".LBL").addClass("countCharRed");

          }

          
          if (cur <= 2) {
              $(".LBL").addClass("countCharRed");

              $(".ApproveT").attr("disabled", "true");
              $(".ApproveT").fadeTo("fast", 0.3)

              $(".ApproveF").attr("disabled", "true");
              $(".ApproveF").fadeTo("fast", 0.3)


          }
          if (cur > 2 && cur < max) {

              $(".LBL").removeClass("countCharRed");
              $(".LBL").addClass("countCharGreen");

              $(".ApproveT").removeAttr("disabled");
              $(".ApproveT").fadeTo("fast", 1)

              $(".ApproveF").removeAttr("disabled");
              $(".ApproveF").fadeTo("fast", 1)

          }


          $(this).next(".maxlength").text(left.toString() + "/" + max);

        return this;
      }).trigger('input');
    });

      
        return this;


  };
}(window.jQuery);
