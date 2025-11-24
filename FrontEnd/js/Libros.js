  // Actualizar contador de libros
        function updateBookCount() {
            const bookCards = document.querySelectorAll('#libros-grid .col-12');
            document.getElementById('total-libros').textContent = bookCards.length;
        }

        // Inicializar cuando se carguen los libros
        setTimeout(updateBookCount, 1000);

        function updateBookCount() {
            const bookCards = document.querySelectorAll('#libros-grid .col-12');
            const totalLibros = document.getElementById('total-libros');
            if (totalLibros) {
                totalLibros.textContent = bookCards.length;
            }
        }

        // Función para inicializar filtros
        function initializeFilters() {
            const searchInput = document.getElementById('searchInput');
            const sortSelect = document.getElementById('sortSelect');

            if (searchInput) {
                searchInput.addEventListener('input', function () {
                    filterBooks();
                });
            }

            if (sortSelect) {
                sortSelect.addEventListener('change', function () {
                    sortBooks();
                });
            }
        }

        // Función para filtrar libros
        function filterBooks() {
            const searchTerm = document.getElementById('searchInput').value.toLowerCase();
            const bookCards = document.querySelectorAll('#libros-grid .col-12');

            bookCards.forEach(card => {
                const title = card.querySelector('.card-title').textContent.toLowerCase();
                if (title.includes(searchTerm)) {
                    card.style.display = 'flex';
                } else {
                    card.style.display = 'none';
                }
            });

            updateBookCount();
        }

        // Función para ordenar libros
        function sortBooks() {
            const sortBy = document.getElementById('sortSelect').value;
            const librosGrid = document.getElementById('libros-grid');
            const bookCards = Array.from(document.querySelectorAll('#libros-grid .col-12'));

            bookCards.sort((a, b) => {
                if (sortBy === 'title') {
                    const titleA = a.querySelector('.card-title').textContent;
                    const titleB = b.querySelector('.card-title').textContent;
                    return titleA.localeCompare(titleB);
                }
                // Para otros tipos de ordenamiento, necesitaríamos más datos
                return 0;
            });

            // Reorganizar los elementos en el grid
            bookCards.forEach(card => librosGrid.appendChild(card));
        }

        // Inicializar cuando la página cargue
        document.addEventListener('DOMContentLoaded', function () {
            initializeFilters();

            // Verificar si los libros ya están cargados
            const bookCards = document.querySelectorAll('#libros-grid .col-12');
            const loadingState = document.getElementById('loadingState');

            if (bookCards.length > 0 && loadingState) {
                loadingState.classList.add('d-none');
                document.getElementById('libros-grid').classList.remove('d-none');
                updateBookCount();
            }
        });

        // También actualizar el contador periódicamente por si hay cambios
        setInterval(updateBookCount, 2000);
