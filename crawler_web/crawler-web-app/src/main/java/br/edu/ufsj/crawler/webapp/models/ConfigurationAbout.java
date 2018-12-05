/**
 * ConfigurationAbout.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.models;

/**
 * Model com as configurações.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 05/12/2018 17:20:36
 * @version $Id$
 */
public class ConfigurationAbout {

    /**
     * Nome da configuração.
     */
    private final String configuration;

    /**
     * Valor da configuração.
     */
    private final String value;

    public ConfigurationAbout(String configuration, String value) {
        this.value = value;
        this.configuration = configuration;
    }

    /**
     * @return the configuration
     */
    public String getConfiguration() {
        return configuration;
    }

    /**
     * @return the value
     */
    public String getValue() {
        return value;
    }
}
