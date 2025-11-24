   // Toggle entre vistas
        document.getElementById('tableView').addEventListener('click', function () {
            document.getElementById('tableViewSection').classList.remove('d-none');
            document.getElementById('cardViewSection').classList.add('d-none');
            this.classList.add('active');
            document.getElementById('cardView').classList.remove('active');
        });

        document.getElementById('cardView').addEventListener('click', function () {
            document.getElementById('tableViewSection').classList.add('d-none');
            document.getElementById('cardViewSection').classList.remove('d-none');
            this.classList.add('active');
            document.getElementById('tableView').classList.remove('active');
        });

        // Función para crear tarjetas de autores
        function createAuthorCards(data) {
            const grid = document.getElementById('authorsGrid');
            let html = '';

            data.forEach(author => {
                const initials = (author.nombre.charAt(0) + author.apellido.charAt(0)).toUpperCase();
                html += `
                    <div class="col-md-6 col-lg-4">
                        <div class="author-card">
                            <div class="author-avatar">
                                ${initials}
                            </div>
                            <h5 class="mb-2">${author.nombre} ${author.apellido}</h5>
                            <p class="text-muted mb-3">${author.pais}, ${author.ciudad}</p>
                            
                            <div class="author-stats">
                                <div class="stat-item">
                                    <div class="stat-number">${Math.floor(Math.random() * 20) + 5}</div>
                                    <div class="stat-label">Obras</div>
                                </div>
                                <div class="stat-item">
                                    <div class="stat-number">${Math.floor(Math.random() * 1000) + 100}</div>
                                    <div class="stat-label">Lectores</div>
                                </div>
                            </div>
                            
                            <div class="mt-3">
                                <span class="badge bg-light text-dark me-2">
                                    <i class="fas fa-phone me-1"></i> ${author.telefono}
                                </span>
                                <span class="badge bg-light text-dark">
                                    <i class="fas fa-map-marker-alt me-1"></i> ${author.estado}
                                </span>
                            </div>
                        </div>
                    </div>
                `;
            });

            grid.innerHTML = html;
        }

        const originalMostrarAutores = window.MostrarAutores;
        window.MostrarAutores = function (data) {
            originalMostrarAutores(data);
            createAuthorCards(data);

            // Ocultar loading state después de crear ambas vistas
            const loadingState = document.getElementById('loadingState');
            if (loadingState) {
                loadingState.classList.add('d-none');
            }
        };