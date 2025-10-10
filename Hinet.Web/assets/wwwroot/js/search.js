$(function () {
    var $form = $('#formSearchHeader');
    var $input = $('#search-header-input');
    var $suggestBox = $('#search-suggestion-box');
    var typingTimer;
    var typingDelay = 500; // ms

    $input.on('keyup', function (e) {
        clearTimeout(typingTimer);
        var keyword = $input.val().trim();

        if (e.key === 'Enter') return;

        if (keyword.length === 0) {
            $suggestBox.addClass('d-none').empty();
            return;
        }

        typingTimer = setTimeout(function () {
            getSuggestions(keyword);
        }, typingDelay);
    });

    $form.on('submit', function (e) {
        var keyword = $input.val().trim();
        if (keyword === '') {
            e.preventDefault();
        }
    });

    function getSuggestions(keyword) {
        const $btn = $form.find('button[type="submit"]');
        showButtonSpinner($btn);

        $.ajax({
            url: '/search/suggestions',
            method: 'GET',
            data: { q: keyword },
            success: function (res) {
                renderSuggestions(res);
            },
            error: function (err) {
                console.error('Lỗi suggestion:', err);
                $suggestBox.addClass('d-none').empty();
            },
            complete: function () {
                hideButtonSpinner($btn);
            }
        });
    }

    function renderSuggestions(data) {
        if (!data || data.length === 0) {
            $suggestBox.addClass('d-none').empty();
            return;
        }

        var html = '';
        data.forEach(function (item) {
            html += `
                <div class="suggest-item" data-url="/mua-acc/${item.Slug}">
                    <i class="bi bi-search text-muted mr-2"></i> ${item.Name}
                </div>
            `;
        });

        $suggestBox.html(html).removeClass('d-none');
    }

    $suggestBox.on('click', '.suggest-item', function () {
        var url = $(this).data('url');
        if (url) {
            window.location.href = url;
        }
    });

    $(document).on('click', function (e) {
        if (!$form.is(e.target) && $form.has(e.target).length === 0) {
            $suggestBox.addClass('d-none');
        }
    });
});
