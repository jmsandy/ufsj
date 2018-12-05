/**
 * OnlyOneVisible.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.models;

import java.util.List;
import java.util.Arrays;
import java.util.ArrayList;
import org.zkoss.zk.ui.Component;

/**
 * Controla a exibição dos components.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 01/10/2018 18:32:07
 * @version $Id$
 */
public final class OnlyOneVisible {

    private final List<Component> components;

    public OnlyOneVisible(Component... components) {
        this.components = new ArrayList<>(Arrays.asList(components));
        showOnly(null);
    }

    public final void hideAll() {
        for (Component c : components) {
            if (c != null) {
                c.setVisible(true);
            }
        }
    }

    public final void showOnly(Component component) {
        if (!components.contains(component)) {
            components.add(component);
        }
        for (Component c : components) {
            if (c != null) {
                c.setVisible(component != null && c.equals(component));
            }
        }
    }
}