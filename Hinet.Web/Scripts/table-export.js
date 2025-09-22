function TableExport(selector, name) {
   var $table = $(selector);
    $table.find("input,select").each(function () {
        $(this).text($(this).val());
    });

    var dataexport = {
        name: name,
        trows: $table
            .find("tr")
            .map(function () {
                var $tds = $(this).find("td:visible, th:visible");
                return {
                    tdatas: $tds
                        .map(function () {
                            var $td = $(this);
                            var bg = $td.css("background-color");
                            if (bg == "rgba(0, 0, 0, 0)") {
                                bg = $td.parent("tr").css("background-color");
                            }
                            return {
                                ttext: _getTText($td),
                                colspan: $td.attr("colspan"),
                                rowspan: $td.attr("rowspan"),
                                width: $td.width(),
                                height: $td.height(),
                                background: _rgb2hex(bg),
                                borderColor: _rgb2hex($td.css("border-color")),
                                textAlign: _textAlign($td),
                                verticalAlign: _verticalAlign($td),
                            };
                        })
                        .get(),
                };
            })
            .get(),
    };
    AjaxCall(
        "/TableExport/SaveModel",
        "post",
        { data: dataexport },
        function () {
            location.href = "/TableExport/Export";
        }
    );

    function _getTText($el) {
        $el =
            $el ?? $("#table-172 > thead > tr:nth-child(1) > th:nth-child(1)");
        var text = $el.contents().not($el.children()).text().trim();
        const data = _getText(text, $el);
        data.children = $el
            .children()
            .map(function () {
                return _getTText($(this));
            })
            .get();

        return data;
    }
    function _getText(text, $el) {
        text = text.replace(/\n {0,}/g, "\n").replace(/\n+/g, "\n");
        var display = $el.css("display");
        return {
            text: text,
            color: _rgb2hex($el.css("color")),
            fontSize: $el.css("font-size").replace("px", ""),
            fontFamily: _getFontFamily($el.css("font-family")),
            bold: $el.css("font-weight") >= 600,
            display: display,
        };
    }

    function _textAlign($el) {
        switch (_getDeepestTextAlign($el)) {
            case "left":
                return 1;
            case "center":
                return 2;
            case "right":
                return 4;
            case "justify":
                return 7;
            default:
                return 0;
        }
    }
    function _getDeepestTextAlign($el) {
        var textAlign = $el.css("text-align");
        if (textAlign && textAlign !== "start") {
            return textAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childTextAlign = _getDeepestTextAlign($($children[i]));
                if (childTextAlign) {
                    return childTextAlign;
                }
            }
        }
        return null;
    }
    function _verticalAlign($el) {
        switch (_getDeepestVerticalAlign($el)) {
            case "top":
                return 0;
            case "middle":
                return 1;
            case "bottom":
                return 2;
            default:
                return 0;
        }
    }
    function _getDeepestVerticalAlign($el) {
        var verticalAlign = $el.css("vertical-align");
        if (verticalAlign && verticalAlign !== "baseline") {
            return verticalAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childVerticalAlign = _getDeepestVerticalAlign(
                    $($children[i])
                );
                if (childVerticalAlign) {
                    return childVerticalAlign;
                }
            }
        }
        return null;
    }
    function _rgb2hex(rgb) {
        if (!rgb || rgb == "rgba(0, 0, 0, 0)") return "";
        rgb = rgb.match(
            /^rgba?[\s+]?\([\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?/i
        );
        return rgb && rgb.length === 4
            ? "#" +
            ("0" + parseInt(rgb[1], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[2], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[3], 10).toString(16)).slice(-2)
            : "";
    }

    function _getFontFamily(fontFamilyString) {
        if (fontFamilyString === null || fontFamilyString === undefined) {
            return "";
        }
        return fontFamilyString.split(",")[0].replace(/'|"/g, "").trim();
    }
}


// export multipletable
function TableExport2(selector, name) {
    var $table = $(selector);
    lstData = [];
    if ($table.length > 0) {
        $table.each(function () {
            $(this).find("input,select").each(function () {
                $(this).text($(this).val());
            });

            var dataexport = {
                name: name,
                trows: $(this)
                    .find("tr")
                    .map(function () {
                        var $tds = $(this).find("td:visible, th:visible");
                        return {
                            tdatas: $tds
                                .map(function () {
                                    var $td = $(this);
                                    var bg = $td.css("background-color");
                                    if (bg == "rgba(0, 0, 0, 0)") {
                                        bg = $td.parent("tr").css("background-color");
                                    }
                                    return {
                                        ttext: _getTText($td),
                                        colspan: $td.attr("colspan"),
                                        rowspan: $td.attr("rowspan"),
                                        width: $td.width(),
                                        height: $td.height(),
                                        background: _rgb2hex(bg),
                                        borderColor: _rgb2hex($td.css("border-color")),
                                        textAlign: _textAlign($td),
                                        verticalAlign: _verticalAlign($td),
                                    };
                                })
                                .get(),
                        };
                    })
                    .get(),
            };
            lstData.push(dataexport);
        })
    }
    
    AjaxCall(
    	"/TableExport/SaveModel2",
    	"post",
    	{ data: lstData },
    	function () {
    		location.href = "/TableExport/Export2";
    	}
    );

    function _getTText($el) {
        $el =
            $el ?? $("#table-172 > thead > tr:nth-child(1) > th:nth-child(1)");
        var text = $el.contents().not($el.children()).text().trim();
        const data = _getText(text, $el);
        data.children = $el
            .children()
            .map(function () {
                return _getTText($(this));
            })
            .get();

        return data;
    }
    function _getText(text, $el) {
        text = text.replace(/\n {0,}/g, "\n").replace(/\n+/g, "\n");
        var display = $el.css("display");
        return {
            text: text,
            color: _rgb2hex($el.css("color")),
            fontSize: $el.css("font-size").replace("px", ""),
            fontFamily: _getFontFamily($el.css("font-family")),
            bold: $el.css("font-weight") >= 600,
            display: display,
        };
    }

    function _textAlign($el) {
        switch (_getDeepestTextAlign($el)) {
            case "left":
                return 1;
            case "center":
                return 2;
            case "right":
                return 4;
            case "justify":
                return 7;
            default:
                return 0;
        }
    }
    function _getDeepestTextAlign($el) {
        var textAlign = $el.css("text-align");
        if (textAlign && textAlign !== "start") {
            return textAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childTextAlign = _getDeepestTextAlign($($children[i]));
                if (childTextAlign) {
                    return childTextAlign;
                }
            }
        }
        return null;
    }
    function _verticalAlign($el) {
        switch (_getDeepestVerticalAlign($el)) {
            case "top":
                return 0;
            case "middle":
                return 1;
            case "bottom":
                return 2;
            default:
                return 0;
        }
    }
    function _getDeepestVerticalAlign($el) {
        var verticalAlign = $el.css("vertical-align");
        if (verticalAlign && verticalAlign !== "baseline") {
            return verticalAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childVerticalAlign = _getDeepestVerticalAlign(
                    $($children[i])
                );
                if (childVerticalAlign) {
                    return childVerticalAlign;
                }
            }
        }
        return null;
    }
    function _rgb2hex(rgb) {
        if (!rgb || rgb == "rgba(0, 0, 0, 0)") return "";
        rgb = rgb.match(
            /^rgba?[\s+]?\([\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?/i
        );
        return rgb && rgb.length === 4
            ? "#" +
            ("0" + parseInt(rgb[1], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[2], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[3], 10).toString(16)).slice(-2)
            : "";
    }

    function _getFontFamily(fontFamilyString) {
        if (fontFamilyString === null || fontFamilyString === undefined) {
            return "";
        }
        return fontFamilyString.split(",")[0].replace(/'|"/g, "").trim();
    }
}

function TableExport3(element, name) {
    var $table = element;
    $table.find("input,select").each(function () {
        $(this).text($(this).val());
    });

    var dataexport = {
        name: name,
        trows: $table
            .find("tr")
            .map(function () {
                var $tds = $(this).find("td:visible, th:visible");
                return {
                    tdatas: $tds
                        .map(function () {
                            var $td = $(this);
                            var bg = $td.css("background-color");
                            if (bg == "rgba(0, 0, 0, 0)") {
                                bg = $td.parent("tr").css("background-color");
                            }
                            return {
                                ttext: _getTText($td),
                                colspan: $td.attr("colspan"),
                                rowspan: $td.attr("rowspan"),
                                width: $td.width(),
                                height: $td.height(),
                                background: _rgb2hex(bg),
                                borderColor: _rgb2hex($td.css("border-color")),
                                textAlign: _textAlign($td),
                                verticalAlign: _verticalAlign($td),
                            };
                        })
                        .get(),
                };
            })
            .get(),
    };
    AjaxCall(
        "/TableExport/SaveModel",
        "post",
        { data: dataexport },
        function () {
            location.href = "/TableExport/Export";
        }
    );

    function _getTText($el) {
        $el =
            $el ?? $("#table-172 > thead > tr:nth-child(1) > th:nth-child(1)");
        var text = $el.contents().not($el.children()).text().trim();
        const data = _getText(text, $el);
        data.children = $el
            .children()
            .map(function () {
                return _getTText($(this));
            })
            .get();

        return data;
    }
    function _getText(text, $el) {
        text = text.replace(/\n {0,}/g, "\n").replace(/\n+/g, "\n");
        var display = $el.css("display");
        return {
            text: text,
            color: _rgb2hex($el.css("color")),
            fontSize: $el.css("font-size").replace("px", ""),
            fontFamily: _getFontFamily($el.css("font-family")),
            bold: $el.css("font-weight") >= 600,
            display: display,
        };
    }

    function _textAlign($el) {
        switch (_getDeepestTextAlign($el)) {
            case "left":
                return 1;
            case "center":
                return 2;
            case "right":
                return 4;
            case "justify":
                return 7;
            default:
                return 0;
        }
    }
    function _getDeepestTextAlign($el) {
        var textAlign = $el.css("text-align");
        if (textAlign && textAlign !== "start") {
            return textAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childTextAlign = _getDeepestTextAlign($($children[i]));
                if (childTextAlign) {
                    return childTextAlign;
                }
            }
        }
        return null;
    }
    function _verticalAlign($el) {
        switch (_getDeepestVerticalAlign($el)) {
            case "top":
                return 0;
            case "middle":
                return 1;
            case "bottom":
                return 2;
            default:
                return 0;
        }
    }
    function _getDeepestVerticalAlign($el) {
        var verticalAlign = $el.css("vertical-align");
        if (verticalAlign && verticalAlign !== "baseline") {
            return verticalAlign;
        } else {
            var $children = $el.children();
            for (var i = 0; i < $children.length; i++) {
                var childVerticalAlign = _getDeepestVerticalAlign(
                    $($children[i])
                );
                if (childVerticalAlign) {
                    return childVerticalAlign;
                }
            }
        }
        return null;
    }
    function _rgb2hex(rgb) {
        if (!rgb || rgb == "rgba(0, 0, 0, 0)") return "";
        rgb = rgb.match(
            /^rgba?[\s+]?\([\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?/i
        );
        return rgb && rgb.length === 4
            ? "#" +
            ("0" + parseInt(rgb[1], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[2], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[3], 10).toString(16)).slice(-2)
            : "";
    }

    function _getFontFamily(fontFamilyString) {
        if (fontFamilyString === null || fontFamilyString === undefined) {
            return "";
        }
        return fontFamilyString.split(",")[0].replace(/'|"/g, "").trim();
    }
}



function getTexts($el) {
    $el = $el ?? $("#table-172 > thead > tr:nth-child(1) > th:nth-child(1)");
    const data = {};
    var text = $el.contents().not($el.children()).text().trim();
    data.text = getText(text, $el);
    data.children = $el
        .children()
        .map(function () {
            return getTexts($(this));
        })
        .get();

    return data;
}
function getText(text, $el) {
    text = text.replace(/\n {0,}/g, "\n").replace(/\n+/g, "\n");
    var display = $el.css("display");
    return {
        text: text,
        color: _rgb2hex($el.css("color")),
        fontSize: $el.css("font-size").replace("px", ""),
        fontFamily: _getFontFamily($el.css("font-family")),
        bold: $el.css("font-weight") >= 600,
    };
}
