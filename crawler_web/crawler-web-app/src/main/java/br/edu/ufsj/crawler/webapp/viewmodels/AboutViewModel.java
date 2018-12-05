/**
 * AboutViewModel.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.viewmodels;

import java.util.List;
import java.util.ArrayList;
import org.zkoss.zk.ui.Execution;
import org.zkoss.bind.annotation.Init;
import org.zkoss.bind.annotation.ContextType;
import org.zkoss.bind.annotation.ContextParam;
import org.zkoss.zk.ui.select.annotation.VariableResolver;
import org.zkoss.zkplus.spring.DelegatingVariableResolver;
import br.edu.ufsj.crawler.webapp.models.ConfigurationAbout;

/**
 * ViewModel para a tela de configuração.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 05/12/2018 17:18:30
 * @version $Id$
 */
@VariableResolver(DelegatingVariableResolver.class)
public class AboutViewModel {

    /**
     * Nome da visão.
     */
    private static final String VIEWNAME = AboutViewModel.class.getSimpleName();

    /**
     * Configurações do processo.
     */
    private List<ConfigurationAbout> configurations;

    @Init()
    public void init(@ContextParam(ContextType.EXECUTION) Execution execution) {
        execution.setAttribute(VIEWNAME, this);

        loadConfigurations();
    }

    /**
     * Carrega as configurações para exibição.
     */
    private void loadConfigurations() {
        configurations = new ArrayList<>();
        configurations.add(new ConfigurationAbout("Database", "crawler_db"));
        configurations.add(new ConfigurationAbout("Objects", "53692 (53,7K)"));
        configurations.add(new ConfigurationAbout("Average Objects", "9.780,324 (9,6K)"));
        configurations.add(new ConfigurationAbout("Data Size", "525.125,151 (0,50GB)"));
        configurations.add(new ConfigurationAbout("MongoDB Version", "3.4.18"));
        configurations.add(new ConfigurationAbout("SOLR Version", "7.5.0"));
    }

    /**
     * @return the configurations
     */
    public List<ConfigurationAbout> getConfigurations() {
        return configurations;
    }

    /**
     * @param configurations the configurations to set
     */
    public void setConfigurations(List<ConfigurationAbout> configurations) {
        this.configurations = configurations;
    }

}
