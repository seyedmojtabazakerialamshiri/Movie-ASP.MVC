$(document).ready(function () {
    $("#addtogenrelist").click(function () {
        var genre = $('#allgenres :selected');
        var index = $('#allgenres option:selected').index();
        if (index !== 0) {
            if ($('#SelectedGenre').find('option').length > 0) {
                if ($("#SelectedGenre option:contains('" + genre.text() + "')").length === 0) {
                    $('#SelectedGenre').append($('<option>',
                        {
                            value: genre.val(),
                            text: genre.text()
                        }));
                }

            }
            else {
                $('#SelectedGenre').append($('<option>',
                    {
                        value: genre.val(),
                        text: genre.text()
                    }));
            }

        }
    });

    $("#cleargenrelist").click(function () {
        $("#SelectedGenre option").each(function () {
            $(this).remove();
        });
    });


    $("#removegenrelist").click(function () {
        $('#SelectedGenre option:selected').remove();
    });

    $("#form").submit(function (e) {
        e.preventDefault();
        $("#OriginalSelectedGenre option").each(function () {
            $(this).attr('selected', true);
        });

        $("#SelectedGenre option").each(function () {
            $(this).attr('selected', true);
        });
        
        this.submit();
        return false;
    });

})