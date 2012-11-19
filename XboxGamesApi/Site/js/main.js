(function (main, $) {

  main.initialize = function () {
    ko.applyBindings(vm);
    vm.loadGenres();

    $(".genre-menu").on("click", ".genre-menu-item", function (e) {
      var genre = $(this).text();
      vm.genre(genre);
      vm.currentPage(1);
      vm.loadGenre();
      e.preventDefault();
      return true;
    });

    $("#nextButton").on("click", function (e) {
      if (!$(this).parent().hasClass("disabled")) {
        vm.currentPage(vm.currentPage() + 1);
        vm.loadGenre();
      }
      e.preventDefault();
      return false;
    });

    $("#prevButton").on("click", function (e) {
      if (!$(this).parent().hasClass("disabled")) {
        vm.currentPage(vm.currentPage() - 1);
        vm.loadGenre();
      }
      e.preventDefault();
      return false;
    });

    $("#firstButton").on("click", function (e) {
      if (!$(this).parent().hasClass("disabled")) {
        vm.currentPage(1);
        vm.loadGenre();
      }
      e.preventDefault();
      return false;
    });

    $("#lastButton").on("click", function (e) {
      if (!$(this).parent().hasClass("disabled")) {
        vm.currentPage(vm.totalPages());
        vm.loadGenre();
      }
      e.preventDefault();
      return false;
    });
  };

  main.showError = function (error) {
    $("#errorAlert").show();
    $("#errorText").text(error);
  };


  var router = $.sammy("#main", function () {

    var routes = [
      {
        path: "#/",
        name: "Games",
        view: "#home-page"
      },
      {
        path: "#/about",
        name: "About",
        view: "#about-page"
      },
    ];

    var me = this;

    function addRoute(route) {
      $(".view").effect("blind", 250, function () {
        $(route.view).show("blind", 250);
      });
      $(".nav li").removeClass("active");
      $(".nav li a:contains(" + route.name + ")").parent().addClass("active");
    }

    $.each(routes, function (i, item) {
      me.get(item.path, function (ctx) {
        addRoute(item);
      });
    });
  });

  $(document).ready(function () {
    router.run("#/");
    main.initialize();
  });

}(window.main = window.main || {}, jQuery));
